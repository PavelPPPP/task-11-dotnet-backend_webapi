using SelfFinanceApp.CollectionEndpoints;

namespace SelfFinanceApp.ServicesEndpoints
{
    public class DailyReportEndpointService
    {
        private WebApplication? _app;
        private ReportEndpoint _endpoint;

        public DailyReportEndpointService(WebApplication? app)
        {
            _app = app;
            _endpoint = new ReportEndpoint();
        }

        public void Map()
        {
            _app?.MapGet(_endpoint.GetReportRoute, _endpoint.GetDataReportFuncAsync);
            _app?.MapGet(_endpoint.GetReportByPeriod, _endpoint.GetDataReportByPeriodFuncAsync);
        }
    }
}
