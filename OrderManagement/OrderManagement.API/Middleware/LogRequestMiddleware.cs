using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Net.Http.Headers;

namespace OrderManagement.API.Middleware
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LogRequestMiddleware> _logger;
        private Func<string, Exception, string> _defaultFormatter = (state, exception) => state;

        public LogRequestMiddleware(RequestDelegate next, ILogger<LogRequestMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // var url = UriHelper.GetDisplayUrl(context.Request);

            //string requestUrlString = $"Request url: {url},Request Method: {context.Request.Method},Request Schem: {context.Request.Scheme}, UserAgent: {context.Request.Headers[HeaderNames.UserAgent].ToString()}";

            var requestIdFeature = context.Features.Get<IHttpRequestIdentifierFeature>();
            if (requestIdFeature?.TraceIdentifier != null)
            {
                // context.Response.Headers["RequestId"] = requestIdFeature.TraceIdentifier;
                this._logger.LogInformation($"\n{DateTime.Now.ToString()}- Request Id: {requestIdFeature.TraceIdentifier}");
            }

            await next(context);
        }
    }
}
