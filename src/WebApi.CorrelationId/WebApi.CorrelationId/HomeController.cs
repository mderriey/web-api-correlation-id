using System.Web.Http;
using Serilog;

namespace WebApi.CorrelationId
{
    public class HomeController : ApiController
    {
        [Route("home")]
        public void Get()
        {
            Log.Information("Executing /home");
        }
    }
}
