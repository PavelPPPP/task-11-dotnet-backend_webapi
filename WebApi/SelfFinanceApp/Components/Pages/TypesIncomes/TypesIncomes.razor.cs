using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;

namespace SelfFinanceApp.Components.Pages.TypesIncomes
{
    public partial class TypesIncomes
    {
        [Parameter]
        public int CurrentPage { get; set; }

        [Inject] protected IConfiguration AppConfig { get; set; } = default!;
        [Inject] IHttpClientFactory ClientFactory { get; set; } = default!;

        string? _requestUriForLoad;
        string? _requestUriForDeleteItem;
        string? _uriAddPageItem;
        string? _uriBaseEditPageItem;

        List<TypeIncomeDTO>? _typesIncomes;
        HttpClient _httpClient = null!;

        protected override async Task OnInitializedAsync()
        {
            string adressHost = AppConfig["AddressHost"] ?? throw new InvalidOperationException("Address Host is invalid!");
            _httpClient = ClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(adressHost);

            _requestUriForLoad = AppConfig["ApiBasePaths:TypesIncomes:List"] ?? throw new InvalidOperationException("Address List TypesIncomes is invalid!");
            _requestUriForDeleteItem = AppConfig["ApiBasePaths:TypesIncomes:Item"] ?? throw new InvalidOperationException("Address TypesIncome item is invalid!");
            _uriAddPageItem = "typesIncomes/add";
            _uriBaseEditPageItem = "typesIncomes/edit/{0}";

            await LoadData();
        }

        async Task LoadData()
        {
            _typesIncomes = await _httpClient.GetFromJsonAsync<List<TypeIncomeDTO>>(_requestUriForLoad) ?? _typesIncomes;

            if (_typesIncomes != null || _typesIncomes!.Count != 0)
            {
                _typesIncomes = _typesIncomes.OrderByDescending(i => i.Id).ToList();
            }
        }
    }
}
