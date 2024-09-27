using InfrastructureApi.DTO;
using SelfFinanceApp.Services.RoutesCollection;
using System.Net;

namespace SelfFinanceApp.Services.ViewModelServices
{
    abstract public class BaseService
    {
        protected HttpClient httpClient;
        protected RoutesCollectionService routesCollection;

        protected BaseService(IHttpClientFactory clientFactory, IConfiguration appConfig, RoutesCollectionService routesCollection)
        {
            string adressHost = appConfig["AddressHost"] ?? throw new InvalidOperationException("Address Host is invalid!");
            httpClient = clientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(adressHost);
            this.routesCollection = routesCollection;
        }

        protected async Task CheckErrorResponse(HttpResponseMessage response)
        {
            ErrorDTO? error;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                error = await response.Content.ReadFromJsonAsync<ErrorDTO>();
                throw new InvalidOperationException($"Status code: {(int)response.StatusCode}\n{error!.Message}");
            }
        }

        protected void CheckReadFromJsonResult(object? result)
        {
            if (result is null)
                throw new InvalidOperationException($"Read JSON content can not be null!");
        }
    }
}
