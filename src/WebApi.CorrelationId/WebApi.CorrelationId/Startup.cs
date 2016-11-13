using System.Web.Http;
using Owin;
using Serilog;
using WebApi.CorrelationId.MessageHandlers;

namespace WebApi.CorrelationId
{
    public class Startup
    {
        public Startup()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level} {CorrelationId}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }

        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();

            httpConfiguration.MessageHandlers.Add(new AddCorrelationIdToLogContextHandler());
            httpConfiguration.MessageHandlers.Add(new AddCorrelationIdToResponseHandler());

            app.UseWebApi(httpConfiguration);
        }
    }
}
