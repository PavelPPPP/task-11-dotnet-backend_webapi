using InfrastructureApi.DTO;
using SelfFinanceBlazor.Components.Pages.BaseEditForm;

namespace SelfFinanceBlazor.Components.Pages.TypesExpenses
{
    public partial class EditFormTypeExpenses : BaseEditFormItem<TypeExpensesDTO>
    {
        public EditFormTypeExpenses()
        {
            itemOrigin = new TypeExpensesDTO();
            itemEdited = new TypeExpensesDTO();
            editContext = new(itemEdited);
        }

        protected internal override void CopyItem()
        {
            itemEdited.Id = itemOrigin.Id;
            itemEdited.Name = itemOrigin.Name;
            itemEdited.Description = itemOrigin.Description;
        }
    }
}
