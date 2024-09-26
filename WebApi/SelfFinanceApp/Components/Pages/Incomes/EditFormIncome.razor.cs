using InfrastructureApi.DTO;
using SelfFinanceApp.Components.Pages.BaseEditForm;
using System.Net;

namespace SelfFinanceApp.Components.Pages.Incomes
{
    public partial class EditFormIncome : BaseEditFormItem<IncomeDTO>
    {
        List<TypeIncomesDTO>? typesIncomes;
        string apiPathGetTypesIncomes = default!;

        public EditFormIncome()
        {
            itemOrigin = new IncomeDTO();
            itemEdited = new IncomeDTO();
            editContext = new(itemEdited);
        }

        protected override void OnInitialized()
        {
            apiPathAddItem = AppConfig["ApiBasePaths:Incomes:Add"] ?? throw new InvalidOperationException("ApiPath Add Income not found!");
            apiPathEditItem = AppConfig["ApiBasePaths:Incomes:Edit"] ?? throw new InvalidOperationException("ApiPath Edit Income not found!");
            apiPathGetItemById = AppConfig["ApiBasePaths:Incomes:Item"] ?? throw new InvalidOperationException("ApiPath Item Income not found!");
            apiPathGetTypesIncomes = AppConfig["ApiBasePaths:TypesIncomes:List"] ?? throw new InvalidOperationException("ApiPath List TypesIncomes not found!");
            base.OnInitialized();
        }

        protected internal override async Task LoadData()
        {
            await GetTypesIncomes();
            await base.LoadData();
        }

        protected internal override void CopyItem()
        {
            itemEdited.Id = itemOrigin.Id;
            itemEdited.TypeId = itemOrigin.TypeId;
            itemEdited.Amount = itemOrigin.Amount;
            itemEdited.Comments = itemOrigin.Comments;
        }

        async Task GetTypesIncomes()
        {
            ErrorDTO? error;
            var responseMsgTypesIncomes = await httpClient.GetAsync(apiPathGetTypesIncomes);

            if (responseMsgTypesIncomes.StatusCode != HttpStatusCode.OK)
            {
                error = await responseMsgTypesIncomes.Content.ReadFromJsonAsync<ErrorDTO>();
                throw new InvalidOperationException($"Status code: {(int)responseMsgTypesIncomes.StatusCode}\n{error?.Message}");
            }

            typesIncomes = await responseMsgTypesIncomes.Content.ReadFromJsonAsync<List<TypeIncomesDTO>>();
        }
    }
}
