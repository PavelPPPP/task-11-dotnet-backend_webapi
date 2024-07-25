using SelfFinanceApp.CollectionEndpoints;

namespace SelfFinanceApp.ServicesEndpoints
{
    public class ExpenseEndpointsService
    {
        private WebApplication? _app;
        private ExpenseEndpoints _expenseEndpoints;
        public ExpenseEndpointsService(WebApplication? app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _expenseEndpoints = new ExpenseEndpoints();
        }

        public void Map()
        {
            _app?.MapGet(_expenseEndpoints.GetAllRoute, _expenseEndpoints.GetAllFunc);
            _app?.MapGet(_expenseEndpoints.GetOrDeleteByIdRoute, _expenseEndpoints.GetByIdFunc);
            _app?.MapPost(_expenseEndpoints.PostAddRoute, _expenseEndpoints.PostAddFunc);
            _app?.MapPut(_expenseEndpoints.PutEditRoute, _expenseEndpoints.PutEditFunc);
            _app?.MapDelete(_expenseEndpoints.GetOrDeleteByIdRoute, _expenseEndpoints.DeleteFunc);
        }
    }
}
