using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Annotations;
using NSwag.AspNetCore;
using NSwag.CodeGeneration.TypeScript;
using NSwag.Generation.AspNetCore;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Vs.Core.Web.OpenApi.v1.Dto.ProtocolErrors;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.OpenApi.Helpers;
using Vs.Rules.OpenApi.v1.Dto;
using Vs.Rules.OpenApi.v1.Features.discipl.Dto;
using ParseResult = Vs.Rules.OpenApi.v1.Dto.ParseResult;

namespace Vs.Rules.OpenApi.v1.Features.vs.Controllers
{
    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// Uses best practices from: https://github.com/RicoSuter/NSwag/wiki/AspNetCoreOpenApiDocumentGenerator
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiVersion("1.0-vs")]
    [Route("api/v{version:apiVersion}/rules")]
    [OpenApiTag("Rules Engine", Description = "This is current api with vs implementations")]
    [ApiController]
    public class RulesControllerVs : ControllerBase
    {
        
    }
}
