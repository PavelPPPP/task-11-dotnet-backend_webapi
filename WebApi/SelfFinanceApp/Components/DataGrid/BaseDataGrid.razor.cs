using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SelfFinanceApp.Services.RouteHistory;
using System.Net;

namespace SelfFinanceApp.Components.DataGrid
{
    public partial class BaseDataGrid<TDataItem>
    {
        [Parameter, EditorRequired]
        public List<TDataItem> Items { get; set; } = default!;
        [Parameter]
        public string PageTitle { get; set; } = default!;
        [Parameter, EditorRequired]
        public string RequestUriForLoadData { get; set; } = default!;
        [Parameter, EditorRequired]
        public Func<TDataItem, string> UriEditPageItem { get; set; } = default!;
        [Parameter, EditorRequired]
        public string UriAddPageItem { get; set; } = default!;
        [Parameter, EditorRequired]
        public Func<TDataItem, string> RequestUriForDeleteItem { get; set; } = default!;

        [Parameter, EditorRequired]
        public RenderFragment? HeaderColumnsTemplate { get; set; }
        [Parameter, EditorRequired]
        public RenderFragment<TDataItem>? ColumnsTemplate { get; set; }

        [Parameter]
        public int CurrentPage { get; set; }
        [Parameter]
        public int PageSize { get; set; } = 5;

        [Inject] protected IConfiguration AppConfig { get; set; } = default!;
        [Inject] IHttpClientFactory ClientFactory { get; set; } = default!;
        [Inject] NavigationManager Navigation { get; set; } = default!;
        [Inject] IJSRuntime JS { get; set; } = default!;
        [Inject] RouteHistoryService RouteHistory { get; set; } = default!;

        List<TDataItem>? ItemsForPage
        {
            get { return Items!.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList(); }
        }

        TDataItem? _selectedItem;

        HttpClient _httpClient = null!;

        string _titleModal = "";
        string _msgModal = "...";
        string _idConfirmModal = "confirmModal";
        string _idInfoModal = "infoModal";

        HttpStatusCode _statusCodeForDeleteItem;

        protected override void OnInitialized()
        {
            string adressHost = AppConfig["AddressHost"] ?? throw new InvalidOperationException("Address Host is invalid!");
            _httpClient = ClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(adressHost);
        }

        void UpdateRouteHistory()
        {
            RouteHistory.Clear();
            RouteHistory.AddRoute(Navigation.Uri);
        }

        void GoToEdit(TDataItem item, Func<TDataItem, string> getUri)
        {
            UpdateRouteHistory();
            string uri = getUri(item);
            Navigation.NavigateTo(uri);
        }

        void GoToCreate()
        {
            UpdateRouteHistory();
            Navigation.NavigateTo(UriAddPageItem);
        }

        void Refresh()
        {
            Navigation.Refresh(true);
        }

        async Task DeleteSelectedItem(TDataItem? selectedItem, Func<TDataItem, string> getRequestUri)
        {
            ErrorDTO? error;

            if (selectedItem is null)
            {
                throw new InvalidOperationException($"Selected item not found!");
            }

            string requestUri = getRequestUri(selectedItem);
            var responseMessage = await _httpClient.DeleteAsync(requestUri);
            _statusCodeForDeleteItem = responseMessage.StatusCode;

            switch (_statusCodeForDeleteItem)
            {
                case HttpStatusCode.OK:
                    {
                        _msgModal = "The selected item has been deleted successfully.";
                        Items?.Remove(selectedItem!);
                        ItemsForPage?.Remove(selectedItem!);
                        await ShowModal(_idInfoModal);
                        return;
                    }
                case HttpStatusCode.NotFound:
                    {
                        error = await responseMessage.Content.ReadFromJsonAsync<ErrorDTO>();
                        _msgModal = error!.Message;
                        Items?.Remove(selectedItem!);
                        ItemsForPage?.Remove(selectedItem!);
                        await ShowModal(_idInfoModal);
                        return;
                    }
                default:
                    {
                        error = await responseMessage.Content.ReadFromJsonAsync<ErrorDTO>();
                        throw new InvalidOperationException($"Status code: {(int)responseMessage.StatusCode}\n{error!.Message}");
                    }
            }
        }

        async Task ShowModal(string idModal)
        {
            await JS.InvokeVoidAsync("showModal", idModal);
        }

        async Task ShowConfirmModal(string idModal, TDataItem item)
        {
            _selectedItem = item;
            await ShowModal(idModal);
        }
    }
}
