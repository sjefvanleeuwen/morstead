using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;
using Vs.Rules.Core;
using Vs.Rules.OpenApi.v1.Dto;
using ParseResult = Vs.Rules.OpenApi.v1.Dto.ParseResult;

namespace Vs.Rules.OpenApi.v2.Controllers
{
    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// Uses best practices from: https://github.com/RicoSuter/NSwag/wiki/AspNetCoreOpenApiDocumentGenerator
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/rules")]
    [OpenApiTag("Rules Engine", Description = "This is current api version")]
    [ApiController]
    public class RulesController : ControllerBase
    {
    }
}
