using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;

namespace SelfFinanceApp.CollectionEndpoints
{
    public class ExpenseEndpoints
    {
        public string GetAllRoute { get; } = "/api/expenses";
        public string GetOrDeleteByIdRoute { get; } = "/api/expenses/{id}";
        public string PostAddRoute { get; } = "/api/expenses/add";
        public string PutEditRoute { get; } = "/api/expenses/edit";

        public ExpenseEndpoints() { }

        public async Task<IEnumerable<ExpenseDTO>> GetAllFunc(IBallanseService<ExpenseDTO> expenseService, ILogger<ExpenseEndpoints> logger)
        {
            logger.LogInformation("Start GET request to get all Expenses");
            var listResult = await expenseService.GetAllAsync();
            logger.LogInformation("End GET request to get all Expenses");
            return listResult;
        }

        public async Task<object> GetByIdFunc(int id, IBallanseService<ExpenseDTO> expenseService, ILogger<ExpenseEndpoints> logger)
        {
            logger.LogInformation("Start GET request to get by id Expense");
            string endRequestMsgLog = "End GET request to get by id Expense";
            var expense = await expenseService.GetByIdAsync(id);
            if (expense is null)
            {
                string warningMsg = "Expense not found";
                logger.LogWarning(warningMsg);
                logger.LogInformation(endRequestMsgLog);
                return Results.NotFound(new { message = warningMsg });
            }

            logger.LogInformation(endRequestMsgLog);
            return expense;
        }

        public async Task<object> PostAddFunc(ExpenseDTO expense, IBallanseService<ExpenseDTO> expenseService, ILogger<ExpenseEndpoints> logger)
        {
            logger.LogInformation("Start POST request to add new Expense");
            await expenseService.CreateAsync(expense);

            logger.LogInformation("End POST request to add new Expense");
            return new { message = "Expense added successed!" };
        }

        public async Task<ExpenseDTO> PutEditFunc(ExpenseDTO expense, IBallanseService<ExpenseDTO> expenseService, ILogger<ExpenseEndpoints> logger)
        {
            logger.LogInformation("Start PUT request to edit exists Expense");
            await expenseService.UpdateAsync(expense);

            logger.LogInformation("End PUT request to edit exists Expense");
            return expense;
        }

        public async Task<object> DeleteFunc(int id, IBallanseService<ExpenseDTO> expenseService, ILogger<ExpenseEndpoints> logger)
        {
            logger.LogInformation("Start DELETE request to remove exists Expense");
            string endRequestMsgLog = "End DELETE request to remove exists Expense";
            var expense = await expenseService.GetByIdAsync(id);
            if (expense is null)
            {
                string warningMsg = "Expense not found";
                logger.LogWarning(warningMsg);
                logger.LogInformation(endRequestMsgLog);
                return Results.NotFound(new { message = warningMsg });
            }

            await expenseService.DeleteAsync(id);

            logger.LogInformation(endRequestMsgLog);
            return expense;
        }
    }
}
