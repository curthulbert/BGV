using System;
using System.Collections.Generic;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.Services
{
    public interface IHoleCountReportDataService
    {
        void GetMillsForHoleCountReport(Action<IEnumerable<int>> getMillsCallback);
        void GetProjectInfoByMillQuery(int mill, Action<IEnumerable<CutterCostView>> getProjectInfoCallback);
        void GetHeaderData(int mill, Action<IEnumerable<int>> submitCallback);
    }
}
