using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;

namespace Acme.Answer.OpenApi.v2.Controllers
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/qa")]
    [OpenApiTag("Answer API", Description = "This is current api")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        [HttpPost("question")]
        public async Task<AnswerPayload> PostQuestion(QuestionPayload payload)
        {
            // uitkeringsgrechtigde.definitieveberekening.maart.2020
            return new AnswerPayload();
        }
    }
}
