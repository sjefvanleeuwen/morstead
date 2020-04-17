using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;

namespace Acme.Answer.OpenApi.v1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/qa")]
    [OpenApiTag("QA API", Description = "")]
    [ApiController]
    public class AnswerController : ControllerBase
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
