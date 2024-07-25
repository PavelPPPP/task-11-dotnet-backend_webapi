using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;

namespace SelfFinanceApp.CollectionEndpoints
{
    public class TypeExpenseEndpoints
    {
        public string GetAllRoute { get; } = "/api/typeExpenses";
        public string GetOrDeleteByIdRoute { get; } = "/api/typeExpenses/{id}";
        public string PostAddRoute { get; } = "/api/typeExpenses/add";
        public string PutEditRoute { get; } = "/api/typeExpenses/edit";

        public TypeExpenseEndpoints() { }

        public async Task<IEnumerable<TypeExpenseDTO>> GetAllFunc(ITypeBaseService<TypeExpenseDTO> typeExpenseService, ILogger<TypeExpenseEndpoints> logger)
        {
            logger.LogInformation("Start GET request to get all TypeExpenses");
            var listResult = await typeExpenseService.GetAllAsync();
            logger.LogInformation("End GET request to get all TypeExpenses");
            return listResult;
        }

        public async Task<object> GetByIdFunc(int id, ITypeBaseService<TypeExpenseDTO> typeExpenseService, ILogger<TypeExpenseEndpoints> logger)
        {
            logger.LogInformation("Start GET request to get by id TypeExpense");
            string endRequestMsgLog = "End GET request to get by id TypeExpense";
            var typeExpense = await typeExpenseService.GetByIdAsync(id);
            if (typeExpense is null)
            {
                string warningMsg = "TypeExpense not found";
                logger.LogWarning(warningMsg);
                logger.LogInformation(endRequestMsgLog);
                return Results.NotFound(new { message = warningMsg });
            }

            logger.LogInformation(endRequestMsgLog);
            return typeExpense;
        }

        public async Task<object> PostAddFunc(TypeExpenseDTO typeExpense, ITypeBaseService<TypeExpenseDTO> typeExpenseService, ILogger<TypeExpenseEndpoints> logger)
        {
            logger.LogInformation("Start POST request to add new TypeExpense");
            await typeExpenseService.CreateAsync(typeExpense);

            logger.LogInformation("End POST request to add new TypeExpense");
            return new { message = "TypeExpense added successed!" };
        }

        public async Task<TypeExpenseDTO> PutEditFunc(TypeExpenseDTO typeExpense, ITypeBaseService<TypeExpenseDTO> typeExpenseService, ILogger<TypeExpenseEndpoints> logger)
        {
            logger.LogInformation("Start PUT request to edit exists TypeExpense");
            await typeExpenseService.UpdateAsync(typeExpense);

            logger.LogInformation("End PUT request to edit exists TypeExpense");
            return typeExpense;
        }

        public async Task<object> DeleteFunc(int id, ITypeBaseService<TypeExpenseDTO> typeExpenseService, ILogger<TypeExpenseEndpoints> logger)
        {
            logger.LogInformation("Start DELETE request to remove exists TypeExpense");
            string endRequestMsgLog = "End DELETE request to remove exists TypeExpense";
            var typeExpense = await typeExpenseService.GetByIdAsync(id);
            if (typeExpense is null)
            {
                string warningMsg = "TypeExpense not found";
                logger.LogWarning(warningMsg);
                logger.LogInformation(endRequestMsgLog);
                return Results.NotFound(new { message = warningMsg });
            }

            await typeExpenseService.DeleteAsync(id);

            logger.LogInformation(endRequestMsgLog);
            return typeExpense;
        }
    }
}
