using Acme.Answer.OpenApi.v2.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;

namespace Acme.Answer.OpenApi.v2.Features.Feature1
{
    [ApiVersion("2.0-feature1")]
    [Route("api/v{version:apiVersion}/qa")]
    [OpenApiTag("Answer API", Description = "This is current api")]
    [ApiController]
    public class AnswerController : Acme.Answer.OpenApi.v2.Controllers.AnswerController
    {
        [HttpPost("some method")]
        public async Task<AnswerPayload> SomeMethod(QuestionPayload payload)
        {
            return new AnswerPayload();
        }
    }
}
