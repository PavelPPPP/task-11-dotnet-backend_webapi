using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SelfFinanceApp.Services.ApiCRUD;
using SelfFinanceApp.Services.RouteHistory;

namespace SelfFinanceApp.Components.DataGrid
{
    public partial class BaseDataGrid<TDataItem> where TDataItem : BaseEntityDTO
    {
        [Parameter, EditorRequired]
        public List<TDataItem> Items { get; set; } = default!;

        [Parameter]
        public string PageTitle { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<TDataItem, string> UriEditPageItem { get; set; } = default!;

        [Parameter, EditorRequired]
        public string UriAddPageItem { get; set; } = default!;

        [Parameter, EditorRequired]
        public RenderFragment? HeaderColumnsTemplate { get; set; }

        [Parameter, EditorRequired]
        public RenderFragment<TDataItem>? ColumnsTemplate { get; set; }

        [Parameter]
        public int CurrentPage { get; set; }
        [Parameter]
        public int PageSize { get; set; } = 5;

        [Inject] NavigationManager Navigation { get; set; } = default!;
        [Inject] IJSRuntime JS { get; set; } = default!;
        [Inject] RouteHistoryService RouteHistory { get; set; } = default!;
        [Inject] EntitiesService ApiCRUD { get; set; } = default!;

        List<TDataItem>? ItemsForPage
        {
            get { return Items!.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList(); }
        }

        TDataItem? _selectedItem;

        string _titleModal = "";
        string _msgModal = "...";
        string _idConfirmModal = "confirmModal";
        string _idInfoModal = "infoModal";

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

        async Task DeleteSelectedItem(TDataItem? selectedItem)
        {
            if (selectedItem is null)
            {
                throw new InvalidOperationException($"Selected item not found!");
            }

            _msgModal = await ApiCRUD.DeleteItem(selectedItem);

            Items?.Remove(selectedItem!);
            ItemsForPage?.Remove(selectedItem!);
            await ShowModal(_idInfoModal);
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
