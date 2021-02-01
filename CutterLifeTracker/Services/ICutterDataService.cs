using System;
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;
using Microsoft.Windows.Data.DomainServices;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.Services
{
    public interface ICutterDataService
    {
        event EventHandler<HasChangesEventArgs> NotifyHasChanges;

        void GetActiveCutterInstanceByCutterId(int cutterId, Action<CutterInstance> getCutterInstanceCallback);
        void GetActiveCutterInstancesByMill(int mill, Action<IEnumerable<CutterInstance>> getCutterInstancesCallback);
        void GetAllCutterInstancesByCutterId(int cutterId, Action<CutterInstance> getCutterInstanceCallback);

        void GetCuttersByMill(int mill, int pageSize, Action<DomainCollectionView<Cutter>> getCuttersCallback);
        void GetAllCutters(int pageSize, Action<DomainCollectionView<Cutter>> getCuttersCallback);
        void CheckIfCutterIdExists(int cutterId, Action<bool> getCutterIdExistsCallback);

        void GetPanelsWithSpecifiedCutterIdAndChangeNumber(CutterInstance cutterInstance, Action<IEnumerable<PanelReportView>> getPanelsCallback);

        void Add(Cutter cutter, CutterInstance cutterInstance, Action<SubmitOperation> submitCallback, object state);
        void Delete(Cutter cutter, Action<SubmitOperation> submitCallback, object state);
        void Save(Action<SubmitOperation> submitCallback, object state);
        void GetMillsForCutterListReport(Action<IEnumerable<int>> submitCallback);
                               
    }
}
