using System;
using System.Collections.ObjectModel;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.DesignModel
{
    public class DesignPanelReportView : ObservableCollection<PanelReportView>
    {
        public DesignPanelReportView()
        {
            for (int i = 0; i < 20; i++)
            {
                Random r = new Random();
                PanelReportView panelReport = new PanelReportView();

                panelReport.CutterID = r.Next(9100, 9200);
                panelReport.CutterChangeNumber = r.Next(30);
                panelReport.MachineNumber = 6;
                panelReport.PanelID = "7588.28." + r.Next(50).ToString();

                Add(panelReport);
            }
        }
    }
}
