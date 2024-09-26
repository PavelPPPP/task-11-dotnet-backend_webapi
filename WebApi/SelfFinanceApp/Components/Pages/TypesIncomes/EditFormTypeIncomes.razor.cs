using InfrastructureApi.DTO;
using SelfFinanceApp.Components.Pages.BaseEditForm;

namespace SelfFinanceApp.Components.Pages.TypesIncomes
{
    public partial class EditFormTypeIncomes : BaseEditFormItem<TypeIncomesDTO>
    {
        public EditFormTypeIncomes()
        {
            itemOrigin = new TypeIncomesDTO();
            itemEdited = new TypeIncomesDTO();
            editContext = new(itemEdited);
        }

        protected override void OnInitialized()
        {
            apiPathAddItem = AppConfig["ApiBasePaths:TypesIncomes:Add"] ?? throw new InvalidOperationException("ApiPath Add Type Income not found!");
            apiPathEditItem = AppConfig["ApiBasePaths:TypesIncomes:Edit"] ?? throw new InvalidOperationException("ApiPath Edit Type Income not found!");
            apiPathGetItemById = AppConfig["ApiBasePaths:TypesIncomes:Item"] ?? throw new InvalidOperationException("ApiPath Item Type Income not found!");
            base.OnInitialized();
        }

        protected internal override void CopyItem()
        {
            itemEdited.Id = itemOrigin.Id;
            itemEdited.Name = itemOrigin.Name;
            itemEdited.Description = itemOrigin.Description;
        }
    }
}
