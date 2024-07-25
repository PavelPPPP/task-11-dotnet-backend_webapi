using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;

namespace SelfFinanceApp.CollectionEndpoints
{
    public class IncomeEndpoints
    {
        public string GetAllRoute { get; } = "/api/incomes";
        public string GetOrDeleteByIdRoute { get; } = "/api/incomes/{id}";
        public string PostAddRoute { get; } = "/api/incomes/add";
        public string PutEditRoute { get; } = "/api/incomes/edit";

        public IncomeEndpoints() { }

        public async Task<IEnumerable<IncomeDTO>> GetAllFunc(IBallanseService<IncomeDTO> incomeService, ILogger<IncomeEndpoints> logger)
        {
            logger.LogInformation("Start GET request to get all Incomes");
            var listResult = await incomeService.GetAllAsync();
            logger.LogInformation("End GET request to get all Incomes");
            return listResult;
        }

        public async Task<object> GetByIdFunc(int id, IBallanseService<IncomeDTO> incomeService, ILogger<IncomeEndpoints> logger)
        {
            logger.LogInformation("Start GET request to get by id Income");
            string endRequestMsgLog = "End GET request to get by id Income";
            var income = await incomeService.GetByIdAsync(id);
            if (income is null) 
            {

                string warningMsg = "Income not found";
                logger.LogWarning(warningMsg);
                logger.LogInformation(endRequestMsgLog);
                return Results.NotFound(new { message = warningMsg }); 
            }

            logger.LogInformation(endRequestMsgLog);
            return income;
        }

        public async Task<object> PostAddFunc(IncomeDTO income, IBallanseService<IncomeDTO> incomeService, ILogger<IncomeEndpoints> logger)
        {
            logger.LogInformation("Start POST request to add new Income");
            await incomeService.CreateAsync(income);

            logger.LogInformation("End POST request to add new Income");
            return new { message = "Income added successed!" };
        }

        public async Task<IncomeDTO> PutEditFunc(IncomeDTO income, IBallanseService<IncomeDTO> incomeService, ILogger<IncomeEndpoints> logger)
        {
            logger.LogInformation("Start PUT request to edit exists Income");
            await incomeService.UpdateAsync(income);
            
            var editedIncome = await incomeService.GetByIdAsync(income.Id);
            logger.LogInformation("End PUT request to edit exists Income");
            return editedIncome;
        }

        public async Task<object> DeleteFunc(int id, IBallanseService<IncomeDTO> incomeService, ILogger<IncomeEndpoints> logger)
        {
            logger.LogInformation("Start DELETE request to remove exists Income");
            string endRequestMsgLog = "End DELETE request to remove exists Income";
            var income = await incomeService.GetByIdAsync(id);
            if (income is null)
            {
                string warningMsg = "Income not found";
                logger.LogWarning(warningMsg);
                logger.LogInformation(endRequestMsgLog);
                return Results.NotFound(new { message = warningMsg });
            }

            await incomeService.DeleteAsync(id);

            logger.LogInformation(endRequestMsgLog);
            return income;
        }
    }
}
