using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using System.Linq;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.Services
{
    internal sealed class PanelReportDataService : DataService, IPanelReportDataService
    {
        private Action<IEnumerable<PanelReportView>> _getPanelReportCallback;
        private LoadOperation _panelInfoLoadOperation;
        private Action<IEnumerable<int>> _getCutterIdsForPanelViewReportCallback;
        private InvokeOperation<IEnumerable<int>> _cutterIdsForPanelViewReportInvokeOperation;

        public void GetPanelReport(Action<IEnumerable<PanelReportView>> getPanelInfoCallback)
        {
            EntityQuery<PanelReportView> panelQuery = _context.GetPanelReportDataOrderedByPanelIDQuery();

            RunPanelReport(panelQuery, getPanelInfoCallback);
        }

        public void GetPanelReportFilteredOnCutterId(int cutterId, Action<IEnumerable<PanelReportView>> getPanelInfoCallback)
        {
            EntityQuery<PanelReportView> panelQuery = _context.GetPanelReportDataFilteredByCutterIdAndOrderedByPanelIdQuery(cutterId);
            RunPanelReport(panelQuery, getPanelInfoCallback);
        }

        private void RunPanelReport(EntityQuery<PanelReportView> query, Action<IEnumerable<PanelReportView>> getPanelInfoCallback)
        {
            _context.PanelReportViews.Clear();
            _getPanelReportCallback = getPanelInfoCallback;
            _panelInfoLoadOperation = _context.Load<PanelReportView>(query);
            _panelInfoLoadOperation.Completed += OnLoadPanelReportCompete;
        }

        private void OnLoadPanelReportCompete(object o, EventArgs e)
        {
            _panelInfoLoadOperation.Completed -= OnLoadPanelReportCompete;

            var panels = _panelInfoLoadOperation.Entities.Cast<PanelReportView>().ToList();
            _getPanelReportCallback(panels);
        }

        public void GetCutterIdsForPanelViewReport(Action<IEnumerable<int>> getCutterIdsForPanelViewReportCallback)
        {
            _getCutterIdsForPanelViewReportCallback = getCutterIdsForPanelViewReportCallback;
            _cutterIdsForPanelViewReportInvokeOperation = _context.GetCuttersUsedInPanelReport();
            _cutterIdsForPanelViewReportInvokeOperation.Completed += OnLoadCuttersForPanelReportCompleted;
        }

        private void OnLoadCuttersForPanelReportCompleted(object o, EventArgs e)
        {
            List<int> cutterIds = new List<int>();

            foreach (int cutterId in _cutterIdsForPanelViewReportInvokeOperation.Value)
                cutterIds.Add(cutterId);

            _cutterIdsForPanelViewReportInvokeOperation.Completed -= OnLoadCuttersForPanelReportCompleted;
            _getCutterIdsForPanelViewReportCallback(cutterIds);
        }
    }
}
