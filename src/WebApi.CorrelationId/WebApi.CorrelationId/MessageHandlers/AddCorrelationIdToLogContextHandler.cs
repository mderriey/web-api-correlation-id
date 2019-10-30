using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Serilog.Context;

namespace WebApi.CorrelationId.MessageHandlers
{
    public class AddCorrelationIdToLogContextHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (LogContext.PushProperty("CorrelationId", request.GetCorrelationId()))
            {
                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}
