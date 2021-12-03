using Infrastructure;
using Service;

namespace SiteActivityReportingService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSiteActivityApi(this IServiceCollection services)
        {
            services.AddService();
            services.AddInfrastructure();

            return services;
        }
    }
}
