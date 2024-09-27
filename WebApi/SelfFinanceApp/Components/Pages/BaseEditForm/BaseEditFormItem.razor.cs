using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SelfFinanceApp.Services.RouteHistory;
using InfrastructureApi.DTO;
using SelfFinanceApp.Services.ApiCRUD;

namespace SelfFinanceApp.Components.Pages.BaseEditForm
{
    public abstract partial class BaseEditFormItem<TItem> where TItem : BaseEntityDTO
    {
        [Parameter]
        public int? Id { get; set; }
        [Parameter]
        public string HeaderPage { set; get; } = "Edit";

        [Inject] IJSRuntime JS { get; set; } = default!;
        [Inject] NavigationManager Navigation { get; set; } = default!;
        [Inject] RouteHistoryService RouteHistory { get; set; } = default!;
        [Inject] protected EntitiesService ApiCRUD { get; set; } = default!;
        

        protected EditContext? editContext;
        protected TItem itemOrigin = default!;
        protected TItem itemEdited = default!;

        private string titleModal = "Confirm saving item";        
        private string msgModal = "...";
        private string idModal = "staticBackdrop";

        private RenderFragment? _renderInputComponetsForm { get; set; }

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
                itemOrigin = await ApiCRUD.GetById<TItem>(Id.Value);
                CopyItem();
            }
        }

        protected internal async Task Submit()
        {
            if (Id is null || Id == 0)
            {
                await ApiCRUD.PostItem<TItem>(itemEdited);
            }
            else
            {
                await ApiCRUD.PutItem<TItem>(itemEdited);
            }

            GoToBack();
        }

        protected void GoToBack()
        {
            var prevPath = RouteHistory.GetPrevPath();
            Navigation.NavigateTo(Navigation.ToBaseRelativePath(prevPath));
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
