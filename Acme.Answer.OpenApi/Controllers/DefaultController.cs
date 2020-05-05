using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Vs.Core.Web.OpenApi;

namespace Acme.Answer.OpenApi.Controllers
{
    [ApiVersion("0.1")]
    [Route("api/v{version:apiVersion}/default")]
    [OpenApiTag("Default API", Description = "")]
    [ApiController]
    public class DefaultController : VsControllerBase
    {
        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToSwaggerUi()
        {
            return Redirect("/swagger/");
        }
    }
}