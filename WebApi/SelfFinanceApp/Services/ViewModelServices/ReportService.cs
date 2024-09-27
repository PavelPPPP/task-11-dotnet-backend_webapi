using InfrastructureApi.DTO.Reports;
using SelfFinanceApp.Services.RoutesCollection;

namespace SelfFinanceApp.Services.ViewModelServices
{
    public class ReportService : BaseService
    {
        public ReportService(IHttpClientFactory clientFactory, IConfiguration appConfig, RoutesCollectionService routesCollection)
            : base(clientFactory, appConfig, routesCollection)
        {
        }

        public async Task<ReportDTO> ReportOnDate(DateTime onDate)
        {
            string requestUri = routesCollection.GetRouteReportOnDate(onDate.ToString("yyyyMMdd"));
            var result = await ExecuteReportRequest(requestUri);

            return result;
        }

        public async Task<ReportDTO> ReportByPeriod(DateTime fromDate, DateTime toDate)
        {
            string requestUri = routesCollection.GetRouteReportByPeriod(fromDate.ToString("yyyyMMdd"), toDate.ToString("yyyyMMdd"));
            var result = await ExecuteReportRequest(requestUri);

            return result;
        }

        private async Task<ReportDTO> ExecuteReportRequest(string requestUri)
        {
            var response = await httpClient.GetAsync(requestUri);
            await CheckErrorResponse(response);
            var result = await response.Content.ReadFromJsonAsync<ReportDTO>();
            CheckReadFromJsonResult(result);

            return result!;
        }
    }
}
