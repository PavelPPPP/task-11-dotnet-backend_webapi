using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;
using SelfFinanceApp.Services.RoutesCollection;

namespace SelfFinanceApp.Common
{
    abstract public class BaseEntityPage<TEntityDTO> : ComponentBase 
        where TEntityDTO : BaseEntityDTO 
    {
        protected string nameApiController;
        protected string namePageController;

        protected string? requestUriForLoad;
        protected string? uriAddPageItem;
        protected string? uriBaseEditPageItem;

        protected List<TEntityDTO>? listEntity;
        
        private HttpClient httpClient = null!;

        [Parameter]
        public int CurrentPage { get; set; }

        [Inject] protected RoutesCollectionService RoutesEntity { get; set; } = default!;
        [Inject] private IConfiguration AppConfig { get; set; } = default!;
        [Inject] private IHttpClientFactory ClientFactory { get; set; } = default!;

        public BaseEntityPage()
        {
            nameApiController = typeof(TEntityDTO).Name
                .DeletePartNameTypeEntity("DTO")
                .LowerFirstChar();
            namePageController = this.GetType().Name
                .LowerFirstChar();
        }

        protected override async Task OnInitializedAsync()
        {
            string adressHost = AppConfig["AddressHost"] ?? throw new InvalidOperationException("Address Host is invalid!");
            httpClient = ClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(adressHost);

            requestUriForLoad = RoutesEntity.GetRouteEntity(nameApiController);
            uriAddPageItem = $"{namePageController}/add";
            uriBaseEditPageItem = namePageController + "/edit/{0}";

            await LoadData();
        }

        protected async Task LoadData()
        {
            listEntity = await httpClient.GetFromJsonAsync<List<TEntityDTO>>(requestUriForLoad) ?? listEntity;

            if (listEntity != null || listEntity!.Count != 0)
            {
                listEntity = listEntity.OrderByDescending(i => i.Id).ToList();
            }
        }
    }
}
