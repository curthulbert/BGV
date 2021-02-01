using System;
using System.Collections.ObjectModel;
using CutterLifeTracker.Model;

namespace CutterLifeTracker.DesignModel
{
    public class DesignPanelReportContainerDataModel : ObservableCollection<PanelReportDataModel>
    {        
        public DesignPanelReportContainerDataModel()
        {
            for (int i = 0; i < 20; i++)
            {
                PanelReportDataModel panelReport = new PanelReportDataModel();
                ObservableCollection<string> cutterList = new ObservableCollection<string>();
                Random r = new Random();
                int count = r.Next(1, 20);

                panelReport.PanelID = "7588.28.01";
                panelReport.EnglishName = "WAC: LH INR WAC";                

                for (int j = 0; j < count; j++)
                {
                    cutterList.Add("95117.01");
                }

                panelReport.CutterList = cutterList;

                Add(panelReport);
            }
        }                
    }
}
