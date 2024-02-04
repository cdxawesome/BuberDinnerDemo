using BuberDinner.Api.Common.Mapping;

namespace BuberDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddMapping();
            services.AddControllers();
            return services;
        }
    }
}
