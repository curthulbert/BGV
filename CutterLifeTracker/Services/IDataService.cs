using System;
using System.ServiceModel.DomainServices.Client;

namespace CutterLifeTracker.Services
{
    public interface IDataService
    {
        event EventHandler<HasChangesEventArgs> NotifyHasChanges;
    }
}
