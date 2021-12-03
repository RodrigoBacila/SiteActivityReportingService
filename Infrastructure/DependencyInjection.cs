using Infrastructure.Activities;
using Microsoft.Extensions.DependencyInjection;
using Service.Activities;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IActivityRepository, ActivityRepository>();

            return services;
        }
    }
}
