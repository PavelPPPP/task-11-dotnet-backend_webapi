using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;

namespace SelfFinanceApp.Components.Pages.Expenses
{
    public partial class Expenses
    {
        [Parameter]
        public int CurrentPage { get; set; }

        [Inject] protected IConfiguration AppConfig { get; set; } = default!;
        [Inject] IHttpClientFactory ClientFactory { get; set; } = default!;

        string? _requestUriForLoad;
        string? _requestUriForDeleteItem;
        string? _uriAddPageItem;
        string? _uriBaseEditPageItem;

        List<ExpenseDTO>? _expenses;
        HttpClient _httpClient = null!;

        protected override async Task OnInitializedAsync()
        {
            string adressHost = AppConfig["AddressHost"] ?? throw new InvalidOperationException("Address Host is invalid!");
            _httpClient = ClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(adressHost);

            _requestUriForLoad = AppConfig["ApiBasePaths:Expenses:List"] ?? throw new InvalidOperationException("Address List Expenses is invalid!");
            _requestUriForDeleteItem = AppConfig["ApiBasePaths:Expenses:Item"] ?? throw new InvalidOperationException("Address Expense item is invalid!");
            _uriAddPageItem = "expenses/add";
            _uriBaseEditPageItem = "expenses/edit/";

            await LoadData();
        }

        async Task LoadData()
        {
            _expenses = await _httpClient.GetFromJsonAsync<List<ExpenseDTO>>(_requestUriForLoad) ?? _expenses;

            if (_expenses != null || _expenses!.Count != 0)
            {
                _expenses = _expenses.OrderByDescending(i => i.Id).ToList();
            }
        }
    }
}
