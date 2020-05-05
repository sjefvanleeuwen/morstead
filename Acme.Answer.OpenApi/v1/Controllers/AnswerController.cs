using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;
using Vs.Core.Web.OpenApi;

namespace Acme.Answer.OpenApi.v1.Controllers
{

    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// Uses best practices from: https://github.com/RicoSuter/NSwag/wiki/AspNetCoreOpenApiDocumentGenerator
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiVersion("1.0-release")]
    [Route("api/v{version:apiVersion}/qa")]
    [OpenApiTag("QA API", Description = "")]
    [ApiController]
    public class AnswerController : VsControllerBase
    {
        /// <summary>
        /// Pings the Rules engine
        /// </summary>
        /// <returns>Pong</returns>
        [HttpGet]
        public async Task<string> Ping()
        {
            return "Pong from v1";
        }
    }
}
