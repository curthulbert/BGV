using System.Collections.ObjectModel;
using CutterLifeTracker.Utils;

namespace CutterLifeTracker.Model
{
    public class PanelReportDataModel : NotifiableObject
    {
        public string PanelID { get; set; }
        public string EnglishName { get; set; }
        public ObservableCollection<string> CutterList { get; set; }

        public PanelReportDataModel()
        {
            CutterList = new ObservableCollection<string>();
        }
    }
}
