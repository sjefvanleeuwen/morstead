using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Annotations;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.TypeScript;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Vs.Core.Web.OpenApi;
using Vs.Core.Web.OpenApi.v1.Dto.CodeGenerators;
using Vs.Core.Web.OpenApi.v1.Dto.ProtocolErrors;

namespace Vs.Rules.OpenApi.v2.Controllers
{
    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// Uses best practices from: https://github.com/RicoSuter/NSwag/wiki/AspNetCoreOpenApiDocumentGenerator
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiVersion("1.0-core")]
    [Route("api/v{version:apiVersion}/core")]
    [OpenApiTag("Code Generation Controller", Description = "Code Generator for API clients")]
    [ApiController]
    public class CodeGenerationController : ControllerBase
    {
        /// <summary>
        /// Generates a typescript client for consuming the API.
        /// </summary>
        /// <param name="request">The request containing the endpoint to the API swagger json contract to generate the code from</param>
        /// <returns>ParesResult</returns>
        /// <response code="200">Typescript api client code Generated</response>
        /// <response code="404">The specified swagger json contract could not be found</response>
        /// <response code="500">Server error</response>
        [HttpPost("generate-type-script-client")]
        [ProducesResponseType(typeof(GenerateTypeScriptClientResponse), 200)]
        [ProducesResponseType(typeof(NotFound404Response), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        [Authorize(Roles = "dev")]
        public async Task<IActionResult> GenerateTypeScriptClient(GenerateTypeScriptClientRequest request)
        {
            try
            {
                OpenApiDocument document;
                try
                {
                    document = await OpenApiDocument.FromUrlAsync(request.SwaggerContractEndpoint.AbsoluteUri);
                }
                catch (HttpRequestException ex)
                {
                    return StatusCode(404, ex.Message);
                }

                var settings = new TypeScriptClientGeneratorSettings
                {
                    ClassName = "{controller}Client",
                };

                var generator = new TypeScriptClientGenerator(document, settings);
                var code = generator.GenerateFile();
                return StatusCode(200, code);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerError500Response(ex) { Endpoint = request.SwaggerContractEndpoint });
            }
        }

        /// <summary>
        /// Generates a csharp client for consuming the API.
        /// </summary>
        /// <param name="request">The request containing the endpoint to the API swagger json contract to generate the code from</param>
        /// <returns>ParesResult</returns>
        /// <response code="200">Typescript api client code Generated</response>
        /// <response code="404">The specified swagger json contract could not be found</response>
        /// <response code="500">Server error</response>
        [HttpPost("generate-csharp-client")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(NotFound404Response), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> GenerateCSharpClient(GenerateCSharpClientRequest request)
        {
            try
            {
                OpenApiDocument document;
                try
                {
                    document = await OpenApiDocument.FromUrlAsync(request.SwaggerContractEndpoint.AbsoluteUri);
                }
                catch (HttpRequestException ex)
                {
                    return StatusCode(404, ex.Message);
                }

                var settings = new CSharpClientGeneratorSettings
                {
                    ClassName = "{controller}Client",
                };

                var generator = new CSharpClientGenerator(document, settings);
                var code = generator.GenerateFile();
                return StatusCode(200, code);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerError500Response(ex) { Endpoint = request.SwaggerContractEndpoint });
            }
        }

        /// <summary>
        /// Generates a csharp server for exposing the API.
        /// </summary>
        /// <param name="request">The request containing the endpoint to the API swagger json contract to generate the code from</param>
        /// <returns>ParesResult</returns>
        /// <response code="200">Typescript api server code Generated</response>
        /// <response code="404">The specified swagger json contract could not be found</response>
        /// <response code="500">Server error</response>
        [HttpPost("generate-csharp-server")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(NotFound404Response), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> GenerateCSharpServer(GenerateCSharpServerRequest request)
        {
            try
            {
                OpenApiDocument document;
                try
                {
                    document = await OpenApiDocument.FromUrlAsync(request.SwaggerContractEndpoint.AbsoluteUri);
                }
                catch (HttpRequestException ex)
                {
                    return StatusCode(404, ex.Message);
                }

                var settings = new CSharpControllerGeneratorSettings
                {
                    ClassName = "{controller}Server",
                };

                var generator = new CSharpControllerGenerator(document, settings);
                var code = generator.GenerateFile();
                return StatusCode(200, code);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerError500Response(ex) { Endpoint = request.SwaggerContractEndpoint });
            }
        }
    }
}
