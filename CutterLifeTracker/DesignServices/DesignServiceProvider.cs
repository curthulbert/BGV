using CutterLifeTracker.Services;

namespace CutterLifeTracker.DesignServices
{
    public class DesignServiceProvider : ServiceProviderBase
    {
        public DesignServiceProvider()
        {            
            CutterDataService = new DesignCutterDataService();
            JSFSkyNetDataService = new DesignJSFSkyNetDataService();
        }
    }
}
