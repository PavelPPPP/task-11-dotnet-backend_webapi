using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;

namespace SelfFinanceApp.CollectionEndpoints
{
    public class TypeIncomeEndpoints
    {
        public string GetAllRoute { get; } = "/api/typeIncomes";
        public string GetOrDeleteByIdRoute { get; } = "/api/typeIncomes/{id}";
        public string PostAddRoute { get; } = "/api/typeIncomes/add";
        public string PutEditRoute { get; } = "/api/typeIncomes/edit";

        public TypeIncomeEndpoints() { }

        public async Task<IEnumerable<TypeIncomeDTO>> GetAllFunc(ITypeBaseService<TypeIncomeDTO> typeIncomeService, ILogger<TypeIncomeEndpoints> logger)
        {
            logger.LogInformation("Start GET request to get all TypeIncomes");
            var listResult = await typeIncomeService.GetAllAsync();
            logger.LogInformation("End GET request to get all TypeIncomes");
            return listResult;
        }

        public async Task<object> GetByIdFunc(int id, ITypeBaseService<TypeIncomeDTO> typeIncomeService, ILogger<TypeIncomeEndpoints> logger)
        {
            logger.LogInformation("Start GET request to get by id TypeIncome");
            string endRequestMsgLog = "End GET request to get by id TypeIncome";
            var typeIncome = await typeIncomeService.GetByIdAsync(id);
            if (typeIncome is null)
            {
                string warningMsg = "TypeIncome not found";
                logger.LogWarning(warningMsg);
                logger.LogInformation(endRequestMsgLog);
                return Results.NotFound(new { message = warningMsg });
            }

            logger.LogInformation(endRequestMsgLog);
            return typeIncome;
        }

        public async Task<object> PostAddFunc(TypeIncomeDTO typeIncome, ITypeBaseService<TypeIncomeDTO> typeIncomeService, ILogger<TypeIncomeEndpoints> logger)
        {
            logger.LogInformation("Start POST request to add new TypeIncome");
            await typeIncomeService.CreateAsync(typeIncome);

            logger.LogInformation("End POST request to add new TypeIncome");
            return new { message = "TypeIncome added successed!" };
        }

        public async Task<TypeIncomeDTO> PutEditFunc(TypeIncomeDTO typeIncome, ITypeBaseService<TypeIncomeDTO> typeIncomeService, ILogger<TypeIncomeEndpoints> logger)
        {
            logger.LogInformation("Start PUT request to edit exists TypeIncome");
            await typeIncomeService.UpdateAsync(typeIncome);

            logger.LogInformation("End PUT request to edit exists TypeIncome");
            return typeIncome;
        }

        public async Task<object> DeleteFunc(int id, ITypeBaseService<TypeIncomeDTO> typeIncomeService, ILogger<TypeIncomeEndpoints> logger)
        {
            logger.LogInformation("Start DELETE request to remove exists TypeIncome");
            string endRequestMsgLog = "End DELETE request to remove exists TypeIncome";
            var typeIncome = await typeIncomeService.GetByIdAsync(id);
            if (typeIncome is null)
            {
                string warningMsg = "TypeIncome not found";
                logger.LogWarning(warningMsg);
                logger.LogInformation(endRequestMsgLog);
                return Results.NotFound(new { message = warningMsg });
            }

            await typeIncomeService.DeleteAsync(id);

            logger.LogInformation(endRequestMsgLog);
            return typeIncome;
        }
    }
}
