using CutterLifeTracker.Services;

namespace CutterLifeTracker.ViewModel
{
    public class ViewModelLocator
    {
        private readonly ServiceProviderBase _sp;
        public CutterListViewModel CLV { get; set; }        
        public HoleCountViewModel HCV { get; set; }
        public PanelReportViewModel PRV { get; set; }
        public CutterHistoryReportViewModel CHR { get; set; }

        public ViewModelLocator()
        {
            _sp = ServiceProviderBase.Instance;
            
            CLV = new CutterListViewModel(_sp.DataService, _sp.CutterDataService);
            HCV = new HoleCountViewModel(_sp.HoleCountReportDataService);
            PRV = new PanelReportViewModel(_sp.CutterDataService, _sp.PanelReportDataService, _sp.JSFSkyNetDataService);
            CHR = new CutterHistoryReportViewModel(_sp.CutterDataService);
        }
    }
}
