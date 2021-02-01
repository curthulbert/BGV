using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using Microsoft.Windows.Data.DomainServices;
using Toolsheet_Library.Web.EntityModel;


namespace CutterLifeTracker.Services
{
    internal sealed class CutterDataService : DataService, ICutterDataService
    {
        #region Variables
                
        private Action<bool> _getCutterIdAvailabilityCallback;
        private InvokeOperation<bool> _cutterIdAvailabilityInvokeOperation;

        private Action<IEnumerable<int>> _getMillsForCutterListReportCallback;                        
        private InvokeOperation<IEnumerable<int>> _millsForCutterListReportInvokeOperation;

        private Action<CutterInstance> _getAllCutterInstancesByCutterIdCallback;
        private LoadOperation _allCutterInstancesByCutterIdLoadOperation;

        private Action<CutterInstance> _getActiveCutterInstanceByCutterIdCallback;
        private LoadOperation _activeCutterInstanceByCutterIdLoadOperation;

        private Action<IEnumerable<CutterInstance>> _getActiveCutterInstancesByMillCallback;
        private LoadOperation _activeCutterInstancesByMillLoadOperation;

        private Action<IEnumerable<PanelReportView>> _getPanelsWithSpecifiedCutterIdAndChangeNumberCallback;
        private LoadOperation _panelsWithSpecifiedCutterIdAndChangeNumberLoadOperation;
        
        /* cutter properties */

        private EntityList<Cutter> _allCuttersSource;
        private EntityQuery<Cutter> _allCuttersQuery;
        private DomainCollectionView<Cutter> _allCuttersView;
        private Action<DomainCollectionView<Cutter>> _getAllCuttersCallback;

        private EntityList<Cutter> _cuttersByMillSource;
        private EntityQuery<Cutter> _cuttersByMillQuery;               
        private DomainCollectionView<Cutter> _cuttersByMillView;
        private Action<DomainCollectionView<Cutter>> _getCuttersByMillCallback;

        #endregion        
               
        public void Delete(Cutter cutter, Action<SubmitOperation> submitCallback, object state)
        {
            _context.Cutters.Remove(cutter);
            _context.SubmitChanges(submitCallback, state);
        }               

        public void Add(Cutter cutter, CutterInstance cutterInstance, Action<SubmitOperation> submitCallback, object state)
        {                                                      
            if ((cutter.EntityState == EntityState.Detached) ||
                (cutterInstance.EntityState == EntityState.Detached))
            {
                //fill in the extra fields.
                cutterInstance.CutterID = cutter.CutterID;                                
                cutterInstance.DateRetired = null;

                //the LifeExpectancy & LifeRemaining of a new cutterInstance will be the
                //same as the cutter
                cutterInstance.LifeExpectancy = cutter.LifeExpectancy;
                cutterInstance.LifeRemaining = cutter.LifeExpectancy;

                if (cutterInstance.DateActivated == null)
                    cutterInstance.DateActivated = DateTime.Today;
                
                _context.Cutters.Add(cutter);
                _context.SubmitChanges(submitCallback, state);
            }            
        }

        public void Save(Action<SubmitOperation> submitCallback, object state)
        {
            if (_context.HasChanges)
                _context.SubmitChanges(submitCallback, state);
        }  

        #region Get List of Mills

        public void GetMillsForCutterListReport(Action<IEnumerable<int>> getMillsCallback)
        {                        
            _getMillsForCutterListReportCallback = getMillsCallback;
            _millsForCutterListReportInvokeOperation = _context.GetCutterListMillValues();
            _millsForCutterListReportInvokeOperation.Completed += OnLoadMillsForCutterListReportCompleted;
        }

        public void OnLoadMillsForCutterListReportCompleted(object o, EventArgs e)
        {
            _millsForCutterListReportInvokeOperation.Completed -= OnLoadMillsForCutterListReportCompleted;

            List<int> mills = new List<int>();
            
            foreach (int mill in _millsForCutterListReportInvokeOperation.Value)
                mills.Add(mill);
            
            _getMillsForCutterListReportCallback(mills);
        }


        #endregion                        

        #region Get CutterInstance Queries

        /// <summary>
        /// Get all cutters instances for a specified cutter id.
        /// </summary>
        /// <param name="cutterId">id of the cutter to return instances for</param>
        /// <param name="getAllCutterInstancesCallback">method name to receive the collection of cutter instances</param>
        public void GetAllCutterInstancesByCutterId(int cutterId, Action<CutterInstance> getAllCutterInstancesCallback)
        {
            var allCutterInstanceQuery = _context.GetCutterInstanceByCutterIdQuery(cutterId);

            _getAllCutterInstancesByCutterIdCallback = getAllCutterInstancesCallback;
            _allCutterInstancesByCutterIdLoadOperation = _context.Load<CutterInstance>(allCutterInstanceQuery);
            _allCutterInstancesByCutterIdLoadOperation.Completed += OnAllCutterInstancesLoadComplete;
        }

        private void OnAllCutterInstancesLoadComplete(object o, EventArgs e)
        {
            _allCutterInstancesByCutterIdLoadOperation.Completed -= OnAllCutterInstancesLoadComplete;

            var cutterInstances = new List<CutterInstance>();

            foreach (var instance in _allCutterInstancesByCutterIdLoadOperation.Entities)
                cutterInstances.Add(instance as CutterInstance);
 
            if (cutterInstances.Count > 0)
                _getAllCutterInstancesByCutterIdCallback(cutterInstances[0]);
        }

         /* An "active" cutter instance, is a cutter instance that hasn't been retired. It's DateRetired field will be null. */

        /// <summary>
        /// Get the cutter instance that hasn't been retired.
        /// </summary>
        /// <param name="cutterId">cutter we are interested in</param>
        /// <param name="getCutterInstanceCallback">method to receive the active cutter instance</param>
        public void GetActiveCutterInstanceByCutterId(int cutterId, Action<CutterInstance> getActiveCutterInstanceCallback)
        {
            var activeCutterInstanceQuery = _context.GetCutterInstanceByCutterIdQuery(cutterId);
            
            var query = activeCutterInstanceQuery.Where(d => d.DateRetired == null);
                                    
            _getActiveCutterInstanceByCutterIdCallback = getActiveCutterInstanceCallback;
            _activeCutterInstanceByCutterIdLoadOperation = _context.Load<CutterInstance>(activeCutterInstanceQuery);
            _activeCutterInstanceByCutterIdLoadOperation.Completed += OnCutterInstanceLoadComplete;
        }

        private void OnCutterInstanceLoadComplete(object o, EventArgs e)
        {
            _activeCutterInstanceByCutterIdLoadOperation.Completed -= OnCutterInstanceLoadComplete;

            List<CutterInstance> cutterInstances = new List<CutterInstance>();

            foreach (var instance in _activeCutterInstanceByCutterIdLoadOperation.Entities)
                cutterInstances.Add(instance as CutterInstance);

            if (cutterInstances.Count > 0)
                _getActiveCutterInstanceByCutterIdCallback(cutterInstances[0]);
        }
        
        /// <summary>
        /// Load collection of cutter instances that have not been retired.
        /// </summary>
        /// <param name="mill">mill number to return collection for</param>
        /// <param name="getCutterActiveInstanceByMillCallback">method to receive collection</param>
        public void GetActiveCutterInstancesByMill(int mill, Action<IEnumerable<CutterInstance>> getActiveCutterInstancesByMillCallback)
        {
            ClearCutterInstances();

            _getActiveCutterInstancesByMillCallback = getActiveCutterInstancesByMillCallback;

            var cutterInstanceQuery = _context.GetActiveCutterInstancesByMillQuery(mill);
            _activeCutterInstancesByMillLoadOperation = _context.Load<CutterInstance>(cutterInstanceQuery);
            _activeCutterInstancesByMillLoadOperation.Completed += OnLoadCutterInstancesCompleted;
        }

        private void OnLoadCutterInstancesCompleted(object o, EventArgs e)
        {
            _activeCutterInstancesByMillLoadOperation.Completed -= OnLoadCutterInstancesCompleted;

            List<CutterInstance> cutterInstances = new List<CutterInstance>();

            foreach (var cutterInstance in _activeCutterInstancesByMillLoadOperation.Entities)
                cutterInstances.Add(cutterInstance as CutterInstance);

            _getActiveCutterInstancesByMillCallback(cutterInstances);
        }

        #endregion

        #region Get Cutter Queries        

        public void CheckIfCutterIdExists(int cutterId, Action<bool> getCutterIdExistsCallback)
        {
            _getCutterIdAvailabilityCallback = getCutterIdExistsCallback;
            _cutterIdAvailabilityInvokeOperation = _context.IsCutterIdAvailable(cutterId);
            _cutterIdAvailabilityInvokeOperation.Completed += OnCutterIdExistsCompleted;
        }

        private void OnCutterIdExistsCompleted(object o, EventArgs e)
        {
            _cutterIdAvailabilityInvokeOperation.Completed -= OnCutterIdExistsCompleted;
            _getCutterIdAvailabilityCallback(_cutterIdAvailabilityInvokeOperation.Value);
        }

        /// <summary>
        /// Load all cutters in to a collection that can be sorted, filtered and paged.
        /// </summary>
        /// <param name="pageSize">page size to return</param>
        /// <param name="getCuttersCallback">method name to return collection to, returns null on error.</param>
        public void GetAllCutters(int pageSize, Action<DomainCollectionView<Cutter>> getCuttersCallback)
        {
            _allCuttersQuery = _context.GetCutterQuery();
            _getAllCuttersCallback = getCuttersCallback;

            var loader = new DomainCollectionViewLoader<Cutter>(OnLoadAllCutters, OnLoadAllCuttersCompleted);
            _allCuttersSource = new EntityList<Cutter>(_context.Cutters);

            _allCuttersView = new DomainCollectionView<Cutter>(loader, _allCuttersSource);


            using (_allCuttersView.DeferRefresh())
            {
                _allCuttersView.PageSize = pageSize;
                _allCuttersView.MoveToFirstPage();
            }
        }

        private LoadOperation<Cutter> OnLoadAllCutters()
        {
            return _context.Load<Cutter>(_allCuttersQuery.SortAndPageBy(_allCuttersView));
        }

        private void OnLoadAllCuttersCompleted(LoadOperation<Cutter> op)
        {
            if (op.HasError)
            {
                op.MarkErrorAsHandled();
                _getAllCuttersCallback(null);
            }
            else if (!op.IsCanceled)
            {
                _allCuttersSource.Source = op.Entities;

                if (op.TotalEntityCount != -1)
                    _allCuttersView.SetTotalItemCount(op.TotalEntityCount);

                _getAllCuttersCallback(_allCuttersView);                
            }
        }
        
        /// <summary>
        /// Load cutters for specified mill in to a collection that can be sorted, filtered and paged.
        /// </summary>
        /// <param name="mill">specifies mill to load cutters from</param>
        /// <param name="pageSize">page size to return</param>
        /// <param name="getCuttersCallback">method name to return collection to, returns null on error.</param>                        
        public void GetCuttersByMill(int mill, int pageSize, Action<DomainCollectionView<Cutter>> getCuttersCallback)
        {
            ClearCutters();

            _cuttersByMillQuery = _context.GetCutterTypesByMillQuery(mill);            
            _getCuttersByMillCallback = getCuttersCallback;
                        
            // Build the DomainCollectionViewLoader
            var loader = new DomainCollectionViewLoader<Cutter>(OnLoadCuttersByMill, OnLoadCuttersByMillCompleted);
            _cuttersByMillSource = new EntityList<Cutter>(_context.Cutters);
            
            _cuttersByMillView = new DomainCollectionView<Cutter>(loader, _cuttersByMillSource);

            using (_cuttersByMillView.DeferRefresh())
            {
                _cuttersByMillView.PageSize = pageSize;
                _cuttersByMillView.MoveToFirstPage();
            }
        }

        private LoadOperation<Cutter> OnLoadCuttersByMill()
        {
            return _context.Load<Cutter>(_cuttersByMillQuery.SortAndPageBy(_cuttersByMillView));
        }

        private void OnLoadCuttersByMillCompleted(LoadOperation<Cutter> op)
        {
            //silently handle errors &
            //send back an empty set
            if (op.HasError)
            {
                op.MarkErrorAsHandled();
                _getCuttersByMillCallback(null);
            }
            else if (!op.IsCanceled)
            {
                _cuttersByMillSource.Source = op.Entities;

                if (op.TotalEntityCount != -1)
                    _cuttersByMillView.SetTotalItemCount(op.TotalEntityCount);

                _getCuttersByMillCallback(_cuttersByMillView);
            }                                
        }

        #endregion        

        public void GetPanelsWithSpecifiedCutterIdAndChangeNumber(CutterInstance cutterInstance, Action<IEnumerable<PanelReportView>> getPanelsCallback)
        {
            int cutterId = cutterInstance.CutterID;
            int changeNumber = cutterInstance.ChangeNumber;

            EntityQuery<PanelReportView> panelReportQuery = _context.GetPanelReportDataFilteredByCutterIdAndOrderedByPanelIdQuery(cutterId);

            //refine query - we only care about the panels that have the correct cutterId & changeNumber
            var query = panelReportQuery.Where(c => c.CutterID == cutterId && c.CutterChangeNumber == changeNumber);

            _getPanelsWithSpecifiedCutterIdAndChangeNumberCallback = getPanelsCallback;
            _panelsWithSpecifiedCutterIdAndChangeNumberLoadOperation = _context.Load<PanelReportView>(query);
            _panelsWithSpecifiedCutterIdAndChangeNumberLoadOperation.Completed += OnPanelsWithSpecifiedCuttersLoadComplete;
        }

        private void OnPanelsWithSpecifiedCuttersLoadComplete(object o, EventArgs e)
        {
            _panelsWithSpecifiedCutterIdAndChangeNumberLoadOperation.Completed -= OnPanelsWithSpecifiedCuttersLoadComplete;
            var panels = new List<PanelReportView>();

            foreach (var instance in _panelsWithSpecifiedCutterIdAndChangeNumberLoadOperation.Entities)
                panels.Add(instance as PanelReportView);

            _getPanelsWithSpecifiedCutterIdAndChangeNumberCallback(panels);
        }

        #region Clear Context

        //reset the cutter & cutterInstance context
        private void ClearCutters()
        {            
            _context.Cutters.Clear();
            ClearCutterInstances();
        }

        private void ClearCutterInstances()
        {
            _context.CutterInstances.Clear();
        }

        #endregion

    }
}
