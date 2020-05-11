using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;
using Vs.Core.Web.OpenApi;

namespace Acme.Answer.OpenApi.v2.Controllers
{
    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// Uses best practices from: https://github.com/RicoSuter/NSwag/wiki/AspNetCoreOpenApiDocumentGenerator
    /// </summary>
    /// <seealso cref="VsControllerBase" />
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/qa")]
    [OpenApiTag("Answer API", Description = "This is current api")]
    [ApiController]
    public class AnswerController : VsControllerBase
    {
        [HttpPost("question")]
        public async Task<AnswerPayload> PostQuestion(QuestionPayload payload)
        {
            // uitkeringsgrechtigde.definitieveberekening.maart.2020
            return new AnswerPayload();
        }
    
        [HttpPost("woonland")]
        public async Task<AnswerPayload> CountryDutch(QuestionPayload payload)
        {
            return new AnswerPayload();//"Nederland"
        }

        [HttpPost("heimat")]
        public async Task<AnswerPayload> CountryGerman(QuestionPayload payload)
        {
            return new AnswerPayload();//"Duitsland"
        }
    }
}
