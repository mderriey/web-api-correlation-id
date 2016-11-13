using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.CorrelationId.MessageHandlers
{
    public class AddCorrelationIdToResponseHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = await base.SendAsync(request, cancellationToken);

            responseMessage
                .Headers
                .Add(CorrelationIdHeaderName, request.GetCorrelationId().ToString());

            return responseMessage;
        }
    }
}
