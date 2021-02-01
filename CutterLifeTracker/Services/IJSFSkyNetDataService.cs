using System;
using System.Collections.Generic;

namespace CutterLifeTracker.Services
{
    public interface IJSFSkyNetDataService
    {
        event EventHandler<HasChangesEventArgs> NotifyHasChanges;
        
        void GetEnglishNameOfPanels(IEnumerable<string> panelIds, Action<IDictionary<string,string>> getEnglishNameOfPanelsCallback);
    }
}
