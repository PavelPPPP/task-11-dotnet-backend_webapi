using InfrastructureApi.DTO;
using SelfFinanceApp.Common;
using SelfFinanceApp.Services.RoutesCollection;
using SelfFinanceApp.Services.ViewModelServices;
using System.Net;

namespace SelfFinanceApp.Services.ApiCRUD
{
    public class EntitiesService : BaseService
    {
        public EntitiesService(IHttpClientFactory clientFactory, IConfiguration appConfig, RoutesCollectionService routesCollection)
            : base(clientFactory, appConfig, routesCollection)
        {
        }

        public async Task<List<TEntityDTO>> GetAll<TEntityDTO>() where TEntityDTO : BaseEntityDTO
        {
            string requestUri = routesCollection.GetRouteEntity(GetNameController(typeof(TEntityDTO)));
            var response = await GetResponseMessage(requestUri);

            await CheckErrorResponse(response);
            var result = await response.Content.ReadFromJsonAsync<List<TEntityDTO>>();
            CheckReadFromJsonResult(result);

            return result!;
        }

        public async Task<TEntityDTO> GetById<TEntityDTO>(int id) where TEntityDTO : BaseEntityDTO
        {
            string requestUri = routesCollection.GetRouteEntity(GetNameController(typeof(TEntityDTO)), id);
            var response = await GetResponseMessage(requestUri);

            await CheckErrorResponse(response);
            var result = await response.Content.ReadFromJsonAsync<TEntityDTO>();
            CheckReadFromJsonResult(result);

            return result!;
        }

        public async Task PostItem<TEntityDTO>(TEntityDTO item) where TEntityDTO : BaseEntityDTO
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            string requestUri = routesCollection.GetRouteEntity(GetNameController(typeof(TEntityDTO)));

            var response = await httpClient.PostAsJsonAsync(requestUri, item);
            await CheckErrorResponse(response);
        }

        public async Task PutItem<TEntityDTO>(TEntityDTO item) where TEntityDTO : BaseEntityDTO
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            string requestUri = routesCollection.GetRouteEntity(GetNameController(typeof(TEntityDTO)), item.Id);

            var response = await httpClient.PutAsJsonAsync(requestUri, item);
            await CheckErrorResponse(response);
        }

        public async Task<string> DeleteItem<TEntityDTO>(TEntityDTO item) where TEntityDTO : BaseEntityDTO
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            ErrorDTO? error;

            string requestUri = routesCollection.GetRouteEntity(GetNameController(typeof(TEntityDTO)), item.Id);

            var response = await httpClient.DeleteAsync(requestUri);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    {
                        return "The selected item has been deleted successfully.";
                    }
                case HttpStatusCode.NotFound:
                    {
                        error = await response.Content.ReadFromJsonAsync<ErrorDTO>();
                        return error!.Message;
                    }
                default:
                    {
                        error = await response.Content.ReadFromJsonAsync<ErrorDTO>();
                        throw new InvalidOperationException($"Status code: {(int)response.StatusCode}\n{error!.Message}");
                    }
            }
        }

        private async Task<HttpResponseMessage> GetResponseMessage(string requestUriForLoad)
        {
            var response = await httpClient.GetAsync(requestUriForLoad);

            await CheckErrorResponse(response);

            return response;
        }

        private string GetNameController(Type type)
        {
            return type.Name
                .DeletePartNameTypeEntity("DTO")
                .LowerFirstChar();
        }
    }
}
