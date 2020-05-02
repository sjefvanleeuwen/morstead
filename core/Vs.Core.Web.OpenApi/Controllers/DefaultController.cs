using Microsoft.AspNetCore.Mvc;

namespace Vs.Core.Web.OpenApi.Controllers
{
    public class DefaultController : Controller
    {
        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToSwaggerUi()
        {
            return Redirect("/swagger/");
        }
    }
}