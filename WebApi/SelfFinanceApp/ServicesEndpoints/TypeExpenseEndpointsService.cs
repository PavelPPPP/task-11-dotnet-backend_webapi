using SelfFinanceApp.CollectionEndpoints;

namespace SelfFinanceApp.ServicesEndpoints
{
    public class TypeExpenseEndpointsService
    {
        private WebApplication? _app;
        private TypeExpenseEndpoints _typeExpenseEndpoints;
        public TypeExpenseEndpointsService(WebApplication? app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _typeExpenseEndpoints = new TypeExpenseEndpoints();
        }

        public void Map()
        {
            _app?.MapGet(_typeExpenseEndpoints.GetAllRoute, _typeExpenseEndpoints.GetAllFunc);
            _app?.MapGet(_typeExpenseEndpoints.GetOrDeleteByIdRoute, _typeExpenseEndpoints.GetByIdFunc);
            _app?.MapPost(_typeExpenseEndpoints.PostAddRoute, _typeExpenseEndpoints.PostAddFunc);
            _app?.MapPut(_typeExpenseEndpoints.PutEditRoute, _typeExpenseEndpoints.PutEditFunc);
            _app?.MapDelete(_typeExpenseEndpoints.GetOrDeleteByIdRoute, _typeExpenseEndpoints.DeleteFunc);
        }
    }
}
