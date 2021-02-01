using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CutterLifeTracker.Services;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.Model
{
    public class PanelReportContainerDataModel
    {
        public IJSFSkyNetDataService JSFSkyNetDataService;
        private Action<IEnumerable<PanelReportDataModel>> _getShapedDataCallback;
        private IEnumerable<PanelReportView> _panelReportCollection;
        private ObservableCollection<string> _panelIds;                     
        private ObservableCollection<PanelReportDataModel> PanelCollection {get; set;}
        

        public PanelReportContainerDataModel()
        {            
            IntializeModel();
        }

        private void IntializeModel()
        {            
            PanelCollection = new ObservableCollection<PanelReportDataModel>();
            _panelIds = new ObservableCollection<string>();          
        }

        public void GetShapedData(IEnumerable<PanelReportView> panelReportCollection, Action<IEnumerable<PanelReportDataModel>> getShapedDataCallback)
        {
            PanelCollection.Clear();

            if (panelReportCollection != null)
            {
                _panelReportCollection = panelReportCollection;
                _getShapedDataCallback = getShapedDataCallback;

                ShapePanelCollection();
            }
        }

        private void ShapePanelCollection()
        {                            
            //group the panels by PanelID
            var panels = from r in _panelReportCollection
                         group r by r.PanelID into p
                         select new { PanelID = p.Key, CutterList = p };
            
            //populate PanelCollection with the details from the query
            foreach (var panel in panels)
            {
                PanelReportDataModel p = new PanelReportDataModel();

                p.PanelID = panel.PanelID;
                _panelIds.Add(panel.PanelID);

                /* combine the cutterID and the cutterChangeNumber for displaying */
                var cutterList = panel.CutterList.Select(i => i.CutterID + "." + i.CutterChangeNumber);

                foreach (var cutter in cutterList)
                    p.CutterList.Add(cutter);

                PanelCollection.Add(p);
            }
            GetEnglishNameOfPanels();                        
        }

        private void GetEnglishNameOfPanels()
        {            
            JSFSkyNetDataService.GetEnglishNameOfPanels(_panelIds, OnGetEnglishNameOfPanelsCallback);            
        }

        private void OnGetEnglishNameOfPanelsCallback(IDictionary<string,string> englishNameOfPanels)
        {
            var englishNameDictionary = new Dictionary<string, string>();            

            if (englishNameOfPanels != null)
            {
                 foreach (var englishName in englishNameOfPanels)
                 {
                     englishNameDictionary.Add(englishName.Key, englishName.Value);
                 }

                 //use the dictionary to lookup the appropriate values for the
                 //project numbers                  
                foreach (var panel in PanelCollection)
                {
                    if (englishNameDictionary.ContainsKey(panel.PanelID))
                    {
                        panel.EnglishName = englishNameDictionary[panel.PanelID];
                    }
                }                
            }
            _getShapedDataCallback(PanelCollection);
        }
    }
}
