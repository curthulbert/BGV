using System;
using System.Collections.Generic;
using CutterLifeTracker.Services;

namespace CutterLifeTracker.DesignServices
{
    public class DesignJSFSkyNetDataService : IJSFSkyNetDataService
    {

        public event EventHandler<HasChangesEventArgs> NotifyHasChanges;

        public void GetEnglishNameOfPanels(IEnumerable<string> panelIds, Action<IDictionary<string,string>> getEnglishNameOfPanelsCallback)
        {
            Dictionary<string,string> englishNames = new Dictionary<string,string>();

            Random r = new Random();
            
            for (int i = 0; i < 20; i++)
            {
                string n = r.Next(50).ToString();
                englishNames.Add(n,"WAC: LH INR WAC");
            }

            getEnglishNameOfPanelsCallback(englishNames);
        }
    }
}
