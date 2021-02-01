using CutterLifeTracker.Utils;
using Toolsheet_Library.Web.EntityModel;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CutterLifeTracker.Model
{
    public class CutterHistoryDataModel : NotifiableObject
    {
        private CutterInstance cutterInstance;
        private IEnumerable<PanelReportView> panels;
            
        public string CutterID
        {
            get
            {
                return cutterInstance.CutterID.ToString();
            }
        }

        public string CutterInstanceID
        {
            get
            {
                return cutterInstance.CutterInstanceID.ToString();
            }
        }

        public string DateActivated 
        { 
            get
            {
                if (cutterInstance.DateActivated != null)
                {
                    DateTime dateActivated = (DateTime)cutterInstance.DateActivated;
                    return dateActivated.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        
        public string DateRetired 
        { 
            get
            {
                if (cutterInstance.DateRetired != null)
                {
                    DateTime dateRetired = (DateTime)cutterInstance.DateRetired;
                    return dateRetired.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }                
        
        public string LifeExpectancy
        {
            get
            {
                return cutterInstance.LifeExpectancy.ToString();
            }
        }

        //list of panels the cutter was used on
        public List<string> PanelIDs
        {
            get
            {
                if (panels != null)
                {
                    return panels.OrderBy(p => p.PanelID)
                                 .Select(p => p.PanelID)
                                 .ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        //how many holes / inches it drilled / cut
        public string UnitsWorked 
        {
            get
            {
                double unitsWorked = cutterInstance.LifeExpectancy - cutterInstance.LifeRemaining;
                return unitsWorked.ToString();
            }
        }

        public CutterHistoryDataModel(CutterInstance _cutterInstance)
        {
            cutterInstance = _cutterInstance;
        }

        public CutterHistoryDataModel(CutterInstance _cutterInstance, IEnumerable<PanelReportView> _panels) 
            : this(_cutterInstance)
        {            
            panels = _panels;
        }
    }
}
