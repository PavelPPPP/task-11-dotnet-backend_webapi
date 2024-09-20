using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;

namespace SelfFinanceApp.Components.Pages.Incomes
{
    public partial class Incomes
    {
        [Parameter]
        public int CurrentPage { get; set; }

        [Inject] protected IConfiguration AppConfig { get; set; } = default!;
        [Inject] IHttpClientFactory ClientFactory { get; set; } = default!;

        string? _requestUriForLoad;
        string? _requestUriForDeleteItem;
        string? _uriAddPageItem;
        string? _uriBaseEditPageItem;

        List<IncomeDTO>? _incomes;
        HttpClient _httpClient = null!;

        protected override async Task OnInitializedAsync()
        {
            string adressHost = AppConfig["AddressHost"] ?? throw new InvalidOperationException("Address Host is invalid!");
            _httpClient = ClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(adressHost);

            _requestUriForLoad = AppConfig["ApiBasePaths:Incomes:List"] ?? throw new InvalidOperationException("Address List Incomes is invalid!");
            _requestUriForDeleteItem = AppConfig["ApiBasePaths:Incomes:Item"] ?? throw new InvalidOperationException("Address Income item is invalid!");
            _uriAddPageItem = "incomes/add";
            _uriBaseEditPageItem = "incomes/edit/";

            await LoadData();
        }

        async Task LoadData()
        {
            _incomes = await _httpClient.GetFromJsonAsync<List<IncomeDTO>>(_requestUriForLoad) ?? _incomes;

            if (_incomes != null || _incomes!.Count != 0)
            {
                _incomes = _incomes.OrderByDescending(i => i.Id).ToList();
            }
        }
    }
}
