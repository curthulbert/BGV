using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using JSFSkyNet_Library.Web.EntityModel;
using JSFSkyNet_Library.Web.Service;

namespace CutterLifeTracker.Services
{
    public class JSFSkyNetDataService : IJSFSkyNetDataService
    {
        #region Variables
        
        private InvokeOperation<Dictionary<string,string>> _getEnglishNameOfPanelsInvokeOperation;        
        private Action<IDictionary<string,string>> _getEnglishNameOfPanelsCallback;

        private LoadOperation<panel_instance> _getListPanelsLoadOperation;
        private Dictionary<string, List<string>> _panelDictionary;        
        private JSFSkyNetDomainContext _context { get; set; }
        public event EventHandler<HasChangesEventArgs> NotifyHasChanges;

        #endregion

        public JSFSkyNetDataService()
        {
            _context = new JSFSkyNetDomainContext();
            _context.PropertyChanged += ContextPropertyChanged;
        }

        private void ContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (NotifyHasChanges != null)
                NotifyHasChanges(this, new HasChangesEventArgs() { HasChanges = _context.HasChanges });
        }

        #region JSFSkyNetQueries
          
        /// <summary>
        /// Build a dictionary to associate the panel names with the English names
        /// </summary>
        /// <param name="panels">list of panels to be 'translated'</param>
        /// <param name="getEnglishNameOfPanelsCallback">dictionary of panel names to english names</param>
        public void GetEnglishNameOfPanels(IEnumerable<string> panels, Action<IDictionary<string,string>> getEnglishNameOfPanelsCallback)
        {
            //save the project and work order lists so
            //we can use them later
            _panelDictionary = new Dictionary<string, List<string>>();

            if (panels.Count() > 0)
            {
                foreach (var panel in panels)
                {
                    string[] englishName = panel.Split('.');

                    string projectNumber = englishName[0];
                    string workOrderNumber = englishName[1];

                    //add the project number and the work order number to the
                    //panel dictionary
                    if (_panelDictionary.ContainsKey(projectNumber))
                    {
                        var workOrderNumbers = _panelDictionary[projectNumber];

                        if (!workOrderNumbers.Contains(workOrderNumber))
                        {
                            _panelDictionary[projectNumber].Add(workOrderNumber);
                        }
                    }
                    else
                    {
                        _panelDictionary.Add(projectNumber, new List<string>() { workOrderNumber });
                    }
                }

                _getEnglishNameOfPanelsCallback = getEnglishNameOfPanelsCallback;
                EntityQuery<panel_instance> query = _context.GetPanelInstancesGivenProjectNumbersQuery(_panelDictionary.Keys);
                
                _getListPanelsLoadOperation = _context.Load<panel_instance>(query);
                _getListPanelsLoadOperation.Completed += OnGetPanelInstanceGivenProjectNumbersLoadCompleted;
            }
            else
            {
                getEnglishNameOfPanelsCallback(null);
            }

        }

        private void OnGetPanelInstanceGivenProjectNumbersLoadCompleted(object o, EventArgs e)
        {
            _getListPanelsLoadOperation.Completed -= OnGetPanelInstanceGivenProjectNumbersLoadCompleted;

            List<string> partNumbers = new List<string>();            

            if (_getListPanelsLoadOperation.TotalEntityCount > 0)
            {
                //create list of panels out of the retrieved data
                //filter it on, and group it by, project numbers in the panel dictionary
                var panels = from p in _getListPanelsLoadOperation.Entities
                             where _panelDictionary.Keys.Contains(p.ji_prj_number)
                             group p by p.ji_prj_number;

                foreach (var groupedPanels in panels)
                {                                        
                    var listOfWorkOrderNumbers = _panelDictionary[groupedPanels.Key];

                    //get JSFSkyNet part_numbers from the grouped panels
                    var listOfJSFSkyNetPartNumbers = (from p in groupedPanels
                                                      where listOfWorkOrderNumbers.Contains(p.ji_wo_number)
                                                      select new { JSFSkyNetPartNumber = p.part_number }
                                                     ).Distinct();                    

                    //add the part numbers to a list that will be sent to the
                    //query to retrieve the English names of each panel
                    foreach (var partNumber in listOfJSFSkyNetPartNumbers)
                    {
                        partNumbers.Add(partNumber.JSFSkyNetPartNumber);
                    }                                           
                }                

                //send the filtered list to the query to retrieve the English names
                _getEnglishNameOfPanelsInvokeOperation = _context.GetEnglishPanelNameGivenPartNumber(partNumbers);                              
                _getEnglishNameOfPanelsInvokeOperation.Completed += OnGetEnglishPanelNameGivePartNumberLoadCompleted;
            }
            else //something failed so return null to the caller
                _getEnglishNameOfPanelsCallback(null);
        }

        private void OnGetEnglishPanelNameGivePartNumberLoadCompleted(object o, EventArgs e)
        {
            _getEnglishNameOfPanelsInvokeOperation.Completed -= OnGetEnglishPanelNameGivePartNumberLoadCompleted;
            
            Dictionary<string, string> englishNames = new Dictionary<string, string>();

            if (_getEnglishNameOfPanelsInvokeOperation.Value != null)
            {
                foreach (var name in _getEnglishNameOfPanelsInvokeOperation.Value)
                {
                    englishNames.Add(name.Key, name.Value);
                }
            }                       

            _getEnglishNameOfPanelsCallback(englishNames);
        }

        #endregion
    }
}
