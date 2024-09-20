using InfrastructureApi.DTO;
using InfrastructureApi.Common;
using System.Text.RegularExpressions;

namespace SelfFinanceApp.CollectionEndpoints
{
    public class EntityEndpoints<TypeDTO> 
        where TypeDTO : BaseEntityDTO
    {
        public string GetAllRoute { get; }
        public string GetOrDeleteByIdRoute { get; }
        public string PostAddRoute { get; }
        public string PutEditRoute { get; }

        private string _nameTypeEntity = String.Empty;
        private string _lowerNameTypeEntity = String.Empty;

        public EntityEndpoints()
        {
            _nameTypeEntity = DeletePartNameTypeEntity(typeof(TypeDTO).Name, "DTO");
            _lowerNameTypeEntity = LowerFirstChar(_nameTypeEntity);

            GetAllRoute = $"/api/{_lowerNameTypeEntity}s";
            GetOrDeleteByIdRoute = $"/api/{_lowerNameTypeEntity}s/{{id}}";
            PostAddRoute = $"/api/{_lowerNameTypeEntity}s/add";
            PutEditRoute = $"/api/{_lowerNameTypeEntity}s/edit";
        }

        private string DeletePartNameTypeEntity(string name, string deletePartStr)
        {
            string tempStr = name;
            tempStr = Regex.Replace(tempStr, @$"{deletePartStr}$", "");

            return tempStr;
        }

        private string LowerFirstChar(string str)
        {
            string tempStr = str;
            char firstChar = str[0];
            tempStr = Regex.Replace(tempStr, @"^\w{1}", firstChar.ToString().ToLower());

            return tempStr;
        }

        public async Task<IEnumerable<TypeDTO>> GetAllFunc(IEntityService<TypeDTO> incomeService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start GET request to get all {_nameTypeEntity}s");
            var listResult = await incomeService.GetAllAsync();
            logger.LogInformation($"End GET request to get all {_nameTypeEntity}s");
            return listResult;
        }

        public async Task<object?> GetByIdFunc(int id, IEntityService<TypeDTO> incomeService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start GET request to get by id {_nameTypeEntity}");
            string endRequestMsgLog = $"End GET request to get by id {_nameTypeEntity}";
            var income = await incomeService.GetByIdAsync(id);
            if (income is null)
            {
                return Results.NotFound(NotFoundObjectRequest(logger, endRequestMsgLog));
            }

            logger.LogInformation(endRequestMsgLog);
            return income;
        }

        public async Task<object> PostAddFunc(TypeDTO income, IEntityService<TypeDTO> incomeService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start POST request to add new {_nameTypeEntity}");
            await incomeService.CreateAsync(income);

            logger.LogInformation($"End POST request to add new {_nameTypeEntity}");
            return new { message = $"{_nameTypeEntity} added successed!" };
        }

        public async Task<object?> PutEditFunc(TypeDTO income, IEntityService<TypeDTO> incomeService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start PUT request to edit exists {_nameTypeEntity}");
            string endRequestMsgLog = $"End PUT request to remove exists {_nameTypeEntity}";
            await incomeService.UpdateAsync(income);

            var editedEntity = await incomeService.GetByIdAsync(income.Id);
            if (income is null)
            {
                return Results.NotFound(NotFoundObjectRequest(logger, endRequestMsgLog));
            }
            logger.LogInformation(endRequestMsgLog);
            return editedEntity;
        }

        public async Task<object?> DeleteFunc(int id, IEntityService<TypeDTO> incomeService, ILogger<EntityEndpoints<TypeDTO>> logger)
        {
            logger.LogInformation($"Start DELETE request to remove exists {_nameTypeEntity}");
            string endRequestMsgLog = $"End DELETE request to remove exists {_nameTypeEntity}";
            var income = await incomeService.GetByIdAsync(id);
            if (income is null)
            {
                return Results.NotFound(NotFoundObjectRequest(logger, endRequestMsgLog));
            }

            await incomeService.DeleteAsync(id);

            logger.LogInformation(endRequestMsgLog);
            return income;
        }

        private object NotFoundObjectRequest(ILogger<EntityEndpoints<TypeDTO>> logger, string endRequestMessage, string warningMsg = "")
        {
            if (warningMsg == "") warningMsg = $"{_nameTypeEntity} not found";

            logger.LogError(warningMsg);
            logger.LogInformation(endRequestMessage);

            return new { message = warningMsg };
        }
    }
}
