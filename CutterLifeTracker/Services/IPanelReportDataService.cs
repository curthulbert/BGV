using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.Services
{
    public interface IPanelReportDataService
    {
        void GetPanelReport(Action<IEnumerable<PanelReportView>> getPanelInfoCallback);
        void GetPanelReportFilteredOnCutterId(int cutterId, Action<IEnumerable<PanelReportView>> getPanelInfoCallback);        
        void GetCutterIdsForPanelViewReport(Action<IEnumerable<int>> getCutterIdsForPanelViewReportCallback);        
    }
}