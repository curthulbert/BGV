using System.ComponentModel;
using CutterLifeTracker.DesignServices;

namespace CutterLifeTracker.Services
{
    public abstract class ServiceProviderBase
    {
        public virtual IDataService DataService { get; protected set; }
        public virtual IHoleCountReportDataService HoleCountReportDataService { get; protected set;}
        public virtual IPanelReportDataService PanelReportDataService { get; protected set; }        
        public virtual ICutterDataService CutterDataService { get; protected set; }
        public virtual IJSFSkyNetDataService JSFSkyNetDataService { get; protected set; }

        private static ServiceProviderBase _instance;
        public static ServiceProviderBase Instance
        {
            get { return _instance ?? CreateInstance(); }
        }

        static ServiceProviderBase CreateInstance()
        {
            return _instance = DesignerProperties.IsInDesignTool ?
                (ServiceProviderBase)new DesignServiceProvider() : new ServiceProvider();
        }
    }
}
