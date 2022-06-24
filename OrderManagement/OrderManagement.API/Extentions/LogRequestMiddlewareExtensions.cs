using OrderManagement.API.Middleware;

namespace OrderManagement.API.Extentions
{
    public static class LogRequestMiddlewareExtensions
    {
        public static IApplicationBuilder AddCustomMiddlewareBuilder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequestMiddleware>();
        }
    }
}
