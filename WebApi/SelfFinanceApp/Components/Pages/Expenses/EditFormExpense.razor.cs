using InfrastructureApi.DTO;
using SelfFinanceApp.Components.Pages.BaseEditForm;
using System.Net;

namespace SelfFinanceApp.Components.Pages.Expenses
{
    public partial class EditFormExpense : BaseEditFormItem<ExpenseDTO>
    {
        List<TypeExpensesDTO>? typesExpenses;
        string apiPathGetTypesExpenses = default!;

        public EditFormExpense()
        {
            itemOrigin = new ExpenseDTO();
            itemEdited = new ExpenseDTO();
            editContext = new(itemEdited);
        }

        protected override void OnInitialized()
        {
            apiPathAddItem = AppConfig["ApiBasePaths:Expenses:Add"] ?? throw new InvalidOperationException("ApiPath Add Expense not found!");
            apiPathEditItem = AppConfig["ApiBasePaths:Expenses:Edit"] ?? throw new InvalidOperationException("ApiPath Edit Expense not found!");
            apiPathGetItemById = AppConfig["ApiBasePaths:Expenses:Item"] ?? throw new InvalidOperationException("ApiPath Item Expense not found!");
            apiPathGetTypesExpenses = AppConfig["ApiBasePaths:TypesExpenses:List"] ?? throw new InvalidOperationException("ApiPath List TypesExpenses not found!");
            base.OnInitialized();
        }

        protected internal override async Task LoadData()
        {
            await GetTypesExpenses();
            await base.LoadData();
        }

        protected internal override void CopyItem()
        {
            itemEdited.Id = itemOrigin.Id;
            itemEdited.TypeId = itemOrigin.TypeId;
            itemEdited.Amount = itemOrigin.Amount;
            itemEdited.Comments = itemOrigin.Comments;
        }

        async Task GetTypesExpenses()
        {
            ErrorDTO? error;
            var responseMsgTypesExpenses = await httpClient.GetAsync(apiPathGetTypesExpenses);

            if (responseMsgTypesExpenses.StatusCode != HttpStatusCode.OK)
            {
                error = await responseMsgTypesExpenses.Content.ReadFromJsonAsync<ErrorDTO>();
                throw new InvalidOperationException($"Status code: {(int)responseMsgTypesExpenses.StatusCode}\n{error!.Message}");
            }

            typesExpenses = await responseMsgTypesExpenses.Content.ReadFromJsonAsync<List<TypeExpensesDTO>>();
        }
    }
}
