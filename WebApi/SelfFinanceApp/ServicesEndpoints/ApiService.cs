using InfrastructureApi.DTO;

namespace SelfFinanceApp.ServicesEndpoints
{
    public class ApiService
    {
        private EntityEndpointsService<IncomeDTO> _incomeEndpointsService;
        private EntityEndpointsService<ExpenseDTO> _expenseEndpointsService;
        private EntityEndpointsService<TypeIncomeDTO> _typeIncomeEndpointsService;
        private EntityEndpointsService<TypeExpenseDTO> _typeExpenseEndpointsService;
        private DailyReportEndpointService _dailyReportEndpointService;

        public ApiService(WebApplication? app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            _incomeEndpointsService = new EntityEndpointsService<IncomeDTO>(app);
            _expenseEndpointsService = new EntityEndpointsService<ExpenseDTO>(app);
            _typeIncomeEndpointsService = new EntityEndpointsService<TypeIncomeDTO>(app);
            _typeExpenseEndpointsService = new EntityEndpointsService<TypeExpenseDTO>(app);
            _dailyReportEndpointService = new DailyReportEndpointService(app);
        }

        public void MapApi()
        {
            _incomeEndpointsService.Map();
            _expenseEndpointsService.Map();
            _typeIncomeEndpointsService.Map();
            _typeExpenseEndpointsService.Map();
            _dailyReportEndpointService.Map();
        }
    }
}
