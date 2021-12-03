using Microsoft.Extensions.DependencyInjection;
using Service.Activities;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IActivityService, ActivityService>();

            return services;
        }
    }
}
