using InfrastructureApi.DTO;
using SelfFinanceApp.Components.Pages.BaseEditForm;

namespace SelfFinanceApp.Components.Pages.TypesExpenses
{
    public partial class EditFormTypeExpenses : BaseEditFormItem<TypeExpensesDTO>
    {
        public EditFormTypeExpenses()
        {
            itemOrigin = new TypeExpensesDTO();
            itemEdited = new TypeExpensesDTO();
            editContext = new(itemEdited);
        }

        protected override void OnInitialized()
        {
            apiPathAddItem = AppConfig["ApiBasePaths:TypesExpenses:Add"] ?? throw new InvalidOperationException("ApiPath Add Type Expense not found!");
            apiPathEditItem = AppConfig["ApiBasePaths:TypesExpenses:Edit"] ?? throw new InvalidOperationException("ApiPath Edit Type Expense not found!");
            apiPathGetItemById = AppConfig["ApiBasePaths:TypesExpenses:Item"] ?? throw new InvalidOperationException("ApiPath Item Type Expense not found!");
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
