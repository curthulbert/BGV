
namespace CutterLifeTracker.Services
{
    public class ServiceProvider : ServiceProviderBase
    {
        public ServiceProvider()
        { }

        public override IDataService DataService
        {
            get
            {
                return new DataService();
            }

        }

        public override IPanelReportDataService PanelReportDataService
        {
            get
            {
                return new PanelReportDataService();
            }
            
        }

        public override IHoleCountReportDataService HoleCountReportDataService
        {
            get
            {
                return new HoleCountReportDataService();
            }            
        }

        public override ICutterDataService CutterDataService
        {
            get
            {
                return new CutterDataService();
            }
        }

        public override IJSFSkyNetDataService JSFSkyNetDataService
        {
            get
            {
                return new JSFSkyNetDataService();
            }
        }
    }
}
