namespace OrderManagement.API.Extentions
{
    public class CorsHelper
    {
        public static void ConfigureService(IServiceCollection service, string allowedHostCors)
        {
            var listallowedHostCors = allowedHostCors.Split(";");
            service.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(listallowedHostCors)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));
        }
    }
}
