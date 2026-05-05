using SelfFinanceBlazor.Services.RouteHistory;
using SelfFinanceBlazor.Services.RoutesCollection;
using SelfFinanceBlazor.Services.ViewModelServices;

namespace SelfFinanceBlazor.Providers
{
    public static class ServiceProviderExtensions
    {
        public static void AddRouteHistoryService(this IServiceCollection services)
        {
            services.AddSingleton<RouteHistoryService>();
        }

        public static void AddRoutesApiCollectionService(this IServiceCollection services)
        {
            services.AddSingleton<RoutesCollectionService>();
        }

        public static void AddViewModelServices(this IServiceCollection services)
        {
            services.AddScoped<EntitiesService>();
            services.AddScoped<ReportService>();
        }
    }
}
