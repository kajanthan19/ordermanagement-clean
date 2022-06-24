using OrderManagement.Core;
using OrderManagement.Core.Options;
using OrderManagement.Infrastructure;

namespace OrderManagement.API.Extentions
{
    public static class CustomDependencyInjection
    {
        public static IServiceCollection AddCustomExtentionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AllowedHostCors>(configuration.GetSection("AllowedHostCors"));
            var allowedHostCors = configuration["AllowedHostCors:AllowedSites"].ToString();

            CorsHelper.ConfigureService(services, allowedHostCors);

            services.AddInfrastructureExtentionServices(configuration);
            services.AddCoreExtentionServices(configuration);

            return services;
        }

    }
}
