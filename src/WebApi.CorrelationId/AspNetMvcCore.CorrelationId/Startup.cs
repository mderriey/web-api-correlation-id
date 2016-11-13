using AspNetMvcCore.CorrelationId.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace AspNetMvcCore.CorrelationId
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

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<AddCorrelationIdToLogContextMiddleware>();
            app.UseMiddleware<AddCorrelationIdToResponseMiddleware>();

            app.Run(async context =>
            {
                Log.Information("Executing the HTTP request");
                await context.Response.WriteAsync("Hello World!");
                Log.Information("Finished executing the HTTP request");
            });
        }
    }
}
