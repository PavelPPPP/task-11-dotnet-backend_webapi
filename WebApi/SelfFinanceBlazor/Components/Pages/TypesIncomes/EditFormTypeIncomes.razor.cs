using InfrastructureApi.DTO;
using SelfFinanceBlazor.Components.Pages.BaseEditForm;

namespace SelfFinanceBlazor.Components.Pages.TypesIncomes
{
    public partial class EditFormTypeIncomes : BaseEditFormItem<TypeIncomesDTO>
    {
        public EditFormTypeIncomes()
        {
            itemOrigin = new TypeIncomesDTO();
            itemEdited = new TypeIncomesDTO();
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
