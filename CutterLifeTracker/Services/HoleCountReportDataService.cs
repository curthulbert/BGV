using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.Services
{
    internal sealed class HoleCountReportDataService : DataService, IHoleCountReportDataService
    {
        private Action<IEnumerable<int>> _getMillsForHoleCountReportCallback;
        private InvokeOperation<IEnumerable<int>> _millsForHoleCountReportInvokeOperation;

        private Action<IEnumerable<CutterCostView>> _getProjectInfoByMillCallback;
        private LoadOperation _projectInfoByMillLoadOperation;

        private Action<IEnumerable<int>> _getHeaderCallback;
        private InvokeOperation<IEnumerable<int>> _headerInvokeOperation;

        public void GetMillsForHoleCountReport(Action<IEnumerable<int>> getMillsCallback)
        {
            _getMillsForHoleCountReportCallback = getMillsCallback;
            _millsForHoleCountReportInvokeOperation = _context.GetCutterCostViewMillValues();
            _millsForHoleCountReportInvokeOperation.Completed += OnLoadMillsForHoleCountReportCompleted;
        }

        private void OnLoadMillsForHoleCountReportCompleted(object o, EventArgs e)
        {
            List<int> mills = new List<int>();

            foreach (int mill in _millsForHoleCountReportInvokeOperation.Value)
                mills.Add(mill);

            _millsForHoleCountReportInvokeOperation.Completed -= OnLoadMillsForHoleCountReportCompleted;
            _getMillsForHoleCountReportCallback(mills);
        }

        public void GetProjectInfoByMillQuery(int mill, Action<IEnumerable<CutterCostView>> getProjectInfoCallback)
        {
            _getProjectInfoByMillCallback = getProjectInfoCallback;

            //clear out the context so that we get clean data
            _context.CutterCostViews.Clear();

            EntityQuery<CutterCostView> projectInfoQuery = _context.GetProjectInfoByMillQuery(mill);
            _projectInfoByMillLoadOperation = _context.Load<CutterCostView>(projectInfoQuery);

            _projectInfoByMillLoadOperation.Completed += OnLoadProjectInfoByMillCompleted;
        }

        private void OnLoadProjectInfoByMillCompleted(object o, EventArgs e)
        {
            _projectInfoByMillLoadOperation.Completed -= OnLoadProjectInfoByMillCompleted;

            var projects = _projectInfoByMillLoadOperation.Entities.Cast<CutterCostView>().ToList();

            _getProjectInfoByMillCallback(projects);
        }

        public void GetHeaderData(int mill, Action<IEnumerable<int>> submitCallback)
        {
            _getHeaderCallback = submitCallback;

            _context.CutterCostViews.Clear();

            _headerInvokeOperation = _context.GetCutterTypesUsedInAllPanels(mill);
            _headerInvokeOperation.Completed += OnLoadHeaderDataComplete;
        }

        private void OnLoadHeaderDataComplete(object o, EventArgs e)
        {
            _headerInvokeOperation.Completed -= OnLoadHeaderDataComplete;
            var headers = _headerInvokeOperation.Value.Cast<int>().ToList();
            headers.Sort();

            _getHeaderCallback(headers);
        }
    }
}
