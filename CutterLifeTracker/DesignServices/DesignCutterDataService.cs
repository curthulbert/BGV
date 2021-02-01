using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using CutterLifeTracker.DesignModel;
using CutterLifeTracker.Services;
using Microsoft.Windows.Data.DomainServices;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.DesignServices
{
    public class DesignCutterDataService : ICutterDataService
    {        
        public event EventHandler<HasChangesEventArgs> NotifyHasChanges;        

        public DesignCutterDataService()
        {            
        }

        #region CutterList

        public void GetMillsForCutterListReport(Action<IEnumerable<int>> getMillsCallback)
        {
            List<int> millCollection = new List<int>();
            millCollection.Add(6);
            millCollection.Add(1);

            getMillsCallback(millCollection);
        }

        public void GetMillsForHoleCountReport(Action<IEnumerable<int>> getMillsCallback)
        {
            GetMillsForCutterListReport(getMillsCallback);
        }

        public void GetActiveCutterInstancesByMill(int mill, Action<IEnumerable<CutterInstance>> getCutterInstancesCallback)
        {
            return;
        }

        public void CheckIfCutterIdExists(int cutterId, Action<bool> getCutterIdExistsCallback)
        {
            return;
        }

        //create some random data to view in the designer
        public void GetCuttersByMill(int mill, int pageSize, Action<DomainCollectionView<Cutter>> getCuttersCallback)
        {
            return;// getCuttersCallback(new DesignCutter());
        }

        public void GetCutterInstanceData(Action<IEnumerable<CutterInstance>> getCutterInstancesCallback)
        {
            getCutterInstancesCallback(new DesignCutterInstances());
        }

        public void Add(Cutter cutter, CutterInstance cutterInstance, Action<SubmitOperation> submitCallback, object state)
        {
            submitCallback(null);
        }

        #endregion

        public void Delete(Cutter cutter, Action<SubmitOperation> submitCallback, object state)
        {
            submitCallback(null);
        }

        public void Save(Action<SubmitOperation> submitCallback, object state)
        {
            submitCallback(null);
        }

        #region HoleCount        

        public void GetProjectInfoByMillQuery(int mill, Action<IEnumerable<CutterCostView>> getProjectInfoCallback)
        {            
            getProjectInfoCallback(new List<CutterCostView>());
        }


        #endregion

        public void GetHeaderData(int mill, Action<IEnumerable<int>> submitCallback)
        {
            return;            
        }


        public void GetPanelReport(Action<IEnumerable<PanelReportView>> getPanelInfoCallback)
        {
            getPanelInfoCallback(new DesignPanelReportView());
        }

        public void GetPanelReportFilteredOnCutterId(int filterOn, Action<IEnumerable<PanelReportView>> getPanelInfoCallback)
        {
            return;
        }
        public void GetActiveCutterInstanceByCutterId(int cutterId, Action<CutterInstance> getCutterInstanceCallback)
        {
            return;
        }

        public void GetCutterIdsForPanelViewReport(Action<IEnumerable<int>> submitCallback)
        {
            return;
        }

        public void GetAllCutterInstancesByCutterId(int cutterId, Action<CutterInstance> getCutterInstanceCallback)
        {
            return;
        }

        public void GetPanelsWithSpecifiedCutterIdAndChangeNumber(CutterInstance cutterInstance, Action<IEnumerable<PanelReportView>> getPanelsCallback)
        {
            return;
        }


        public void GetAllCutters(int pageSize, Action<DomainCollectionView<Cutter>> getCuttersCallback)
        {
            return;
        }
    }
}
