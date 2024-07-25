using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;
using SelfFinanceApp.CollectionEndpoints;

namespace SelfFinanceApp.ServicesEndpoints
{
    public class IncomeEndpointsService
    {
        private WebApplication? _app;
        private IncomeEndpoints _incomeEndpoints;
        public IncomeEndpointsService(WebApplication? app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _incomeEndpoints = new IncomeEndpoints();
        }

        public void Map()
        {
            _app?.MapGet(_incomeEndpoints.GetAllRoute, _incomeEndpoints.GetAllFunc);
            _app?.MapGet(_incomeEndpoints.GetOrDeleteByIdRoute, _incomeEndpoints.GetByIdFunc);
            _app?.MapPost(_incomeEndpoints.PostAddRoute, _incomeEndpoints.PostAddFunc);
            _app?.MapPut(_incomeEndpoints.PutEditRoute, _incomeEndpoints.PutEditFunc);
            _app?.MapDelete(_incomeEndpoints.GetOrDeleteByIdRoute, _incomeEndpoints.DeleteFunc);
        }
    }
}
