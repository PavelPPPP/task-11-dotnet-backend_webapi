using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;

namespace SelfFinanceApp.Components.Pages.TypesExpenses
{
    public partial class TypesExpenses
    {
        [Parameter]
        public int CurrentPage { get; set; }

        [Inject] protected IConfiguration AppConfig { get; set; } = default!;
        [Inject] IHttpClientFactory ClientFactory { get; set; } = default!;

        string? _requestUriForLoad;
        string? _requestUriForDeleteItem;
        string? _uriAddPageItem;
        string? _uriBaseEditPageItem;

        List<TypeExpenseDTO>? _typesExpenses;
        HttpClient _httpClient = null!;

        protected override async Task OnInitializedAsync()
        {
            string adressHost = AppConfig["AddressHost"] ?? throw new InvalidOperationException("Address Host is invalid!");
            _httpClient = ClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(adressHost);

            _requestUriForLoad = AppConfig["ApiBasePaths:TypesExpenses:List"] ?? throw new InvalidOperationException("Address List TypesExpenses is invalid!");
            _requestUriForDeleteItem = AppConfig["ApiBasePaths:TypesExpenses:Item"] ?? throw new InvalidOperationException("Address TypesExpense item is invalid!");
            _uriAddPageItem = "typesExpenses/add";
            _uriBaseEditPageItem = "typesExpenses/edit/";

            await LoadData();
        }

        async Task LoadData()
        {
            _typesExpenses = await _httpClient.GetFromJsonAsync<List<TypeExpenseDTO>>(_requestUriForLoad) ?? _typesExpenses;

            if (_typesExpenses != null || _typesExpenses!.Count != 0)
            {
                _typesExpenses = _typesExpenses.OrderByDescending(i => i.Id).ToList();
            }
        }
    }
}
