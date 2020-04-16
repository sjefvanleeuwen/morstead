using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;

namespace Vs.Rules.OpenApi.Controllers
{
    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/rules")]
    [OpenApiTag("Rules Engine", Description = "This is current stable api version")]
    [ApiController]
    public class RulesControllerV2 : ControllerBase
    {
        /// <summary>
        /// Pings the Rules engine
        /// </summary>
        /// <returns>Pong</returns>
        [HttpGet]
        public async Task<string> Ping()
        {
            return "Pong";
        }
    }
}
