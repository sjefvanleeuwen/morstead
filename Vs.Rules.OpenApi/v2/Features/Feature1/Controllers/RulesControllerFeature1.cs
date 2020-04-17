using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Vs.Rules.OpenApi.v2.Controllers;

namespace Vs.Rules.OpenApi.v2.Features.Feature1.Controllers
{
    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// Uses best practices from: https://github.com/RicoSuter/NSwag/wiki/AspNetCoreOpenApiDocumentGenerator
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiVersion("2.0-beerrun")]
    [Route("api/v{version:apiVersion}/rules")]
    [OpenApiTag("Rules Engine", Description = "This is current api with feature 2 implementation")]
    [ApiController]
    public class RulesControllerFeature1 : RulesController
    {
        // specific features for fast delivery
        [HttpPost("order-beer")]
        public void OrderBeer(string beer)
        {

        }
    }
}
