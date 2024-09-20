using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SelfFinanceApp.Services.RouteHistory;
using InfrastructureApi.DTO;
using System.Net;

namespace SelfFinanceApp.Components.Pages.BaseEditForm
{
    public abstract partial class BaseEditFormItem<TItem>
    {
        [Parameter]
        public int? Id { get; set; }
        [Parameter]
        public string HeaderPage { set; get; } = "Edit";

        [Inject] protected IConfiguration AppConfig { get; set; } = default!;
        [Inject] IJSRuntime JS { get; set; } = default!;
        [Inject] NavigationManager Navigation { get; set; } = default!;
        [Inject] RouteHistoryService RouteHistory { get; set; } = default!;
        [Inject] IHttpClientFactory ClientFactory { get; set; } = default!;
        

        protected EditContext? editContext;
        protected TItem itemOrigin = default!;
        protected TItem itemEdited = default!;
        protected HttpClient httpClient = default!;

        protected string apiPathAddItem = default!;
        protected string apiPathEditItem = default!;
        protected string apiPathGetItemById = default!;

        private string titleModal = "Confirm saving item";        
        private string msgModal = "...";
        private string idModal = "staticBackdrop";

        private RenderFragment? _renderInputComponetsForm { get; set; }

        

        protected override void OnInitialized()
        {
            string adressHost = AppConfig["AddressHost"] ?? throw new InvalidOperationException("Address Host is invalid!");
            httpClient = ClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(adressHost);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            _renderInputComponetsForm = RenderInputComponentsForm;
        }

        protected internal abstract void RenderInputComponentsForm(RenderTreeBuilder __builder);
        protected internal abstract void CopyItem();

        protected internal virtual async Task LoadData()
        {
            if (Id is not null && Id != 0)
            {
                await GetItemById(Id);
                CopyItem();
            }
        }

        protected internal async Task Submit()
        {
            ErrorDTO? error;
            HttpResponseMessage response;

            if (Id is null || Id == 0)
            {
                response = await httpClient.PostAsJsonAsync<TItem>(apiPathAddItem, itemEdited);
            }
            else
            {
                response = await httpClient.PutAsJsonAsync<TItem>(apiPathEditItem, itemEdited);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                error = await response.Content.ReadFromJsonAsync<ErrorDTO>();
                throw new InvalidOperationException($"Status code: {(int)response.StatusCode}\n{error!.Message}");
            }

            GoToBack();
        }

        protected void GoToBack()
        {
            var prevPath = RouteHistory.GetPrevPath();
            Navigation.NavigateTo(Navigation.ToBaseRelativePath(prevPath));
        }

        async Task GetItemById(int? id)
        {
            ErrorDTO? error;
            var responseMsgIncome = await httpClient.GetAsync($"{apiPathGetItemById}{id}");

            if (responseMsgIncome.StatusCode != HttpStatusCode.OK)
            {
                error = await responseMsgIncome.Content.ReadFromJsonAsync<ErrorDTO>();
                throw new InvalidOperationException($"Status code: {(int)responseMsgIncome.StatusCode}\n{error!.Message}");
            }

            itemOrigin = await responseMsgIncome.Content.ReadFromJsonAsync<TItem>() ?? itemOrigin;
        }

        async Task ShowModal(string idModal)
        {
            if (editContext != null && editContext.Validate())
            {
                if (Id is null || Id == 0)
                {
                    msgModal = "Please confirm adding data";
                }
                else
                {
                    msgModal = "Please confirm the data changes";
                }
                await JS.InvokeVoidAsync("showModal", idModal);
            }
        }
    }
}
