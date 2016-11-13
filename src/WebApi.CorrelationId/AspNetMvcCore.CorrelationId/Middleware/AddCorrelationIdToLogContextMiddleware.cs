using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace AspNetMvcCore.CorrelationId.Middleware
{
    public class AddCorrelationIdToLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public AddCorrelationIdToLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context, ILogger<AddCorrelationIdToLogContextMiddleware> logger)
        {
            using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
            {
                return _next(context);
            }
        }
    }
}
