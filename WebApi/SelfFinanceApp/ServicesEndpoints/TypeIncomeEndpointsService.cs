using SelfFinanceApp.CollectionEndpoints;

namespace SelfFinanceApp.ServicesEndpoints
{
    public class TypeIncomeEndpointsService
    {
        private WebApplication? _app;
        private TypeIncomeEndpoints _typeIncomeEndpoints;
        public TypeIncomeEndpointsService(WebApplication? app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _typeIncomeEndpoints = new TypeIncomeEndpoints();
        }

        public void Map()
        {
            _app?.MapGet(_typeIncomeEndpoints.GetAllRoute, _typeIncomeEndpoints.GetAllFunc);
            _app?.MapGet(_typeIncomeEndpoints.GetOrDeleteByIdRoute, _typeIncomeEndpoints.GetByIdFunc);
            _app?.MapPost(_typeIncomeEndpoints.PostAddRoute, _typeIncomeEndpoints.PostAddFunc);
            _app?.MapPut(_typeIncomeEndpoints.PutEditRoute, _typeIncomeEndpoints.PutEditFunc);
            _app?.MapDelete(_typeIncomeEndpoints.GetOrDeleteByIdRoute, _typeIncomeEndpoints.DeleteFunc);
        }
    }
}
