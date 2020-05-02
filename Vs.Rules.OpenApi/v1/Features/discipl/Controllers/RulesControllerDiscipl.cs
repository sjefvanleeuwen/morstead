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
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.OpenApi.Helpers;
using Vs.Rules.OpenApi.v1.Dto;
using Vs.Rules.OpenApi.v1.Features.discipl.Dto;
using ParseResult = Vs.Rules.OpenApi.v1.Dto.ParseResult;

namespace Vs.Rules.OpenApi.v1.Features.discipl.Controllers
{
    /// <summary>
    /// Rules API integrates the rule engine and exposes it as OAS3.
    /// Uses best practices from: https://github.com/RicoSuter/NSwag/wiki/AspNetCoreOpenApiDocumentGenerator
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiVersion("1.0-discipl")]
    [Route("api/v{version:apiVersion}/rules")]
    [OpenApiTag("Rules Engine", Description = "This is current api with feature 1 implementation")]
    [ApiController]
    public class RulesControllerDiscipl : ControllerBase
    {
        /// <summary>
        /// Generates the content template for a given yaml rule file.
        /// </summary>
        /// <param name="yaml"></param>
        /// <returns>The content template in yaml format</returns>
        /// <response code="200">Yaml content Template Parsed</response>
        /// <response code="400">Yaml rule set contains errors</response>
        /// <response code="404">Yaml rule set could not be found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(typeof(ParseResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        [HttpPost("generate-content-template")]
        public async Task<IActionResult> GenerateContentTemplate(Uri url)
        {
            try
            {

                YamlScriptController controller = new YamlScriptController();
                string yaml;
                using (var client = new WebClient())
                {
                    try
                    {
                        yaml = client.DownloadString(url);
                    }
                    catch (WebException ex)
                    {
                        return StatusCode(404, ex.Message);
                    }
                }

                var result = controller.Parse(yaml);
                ParseResult parseResult = new ParseResult()
                {
                    IsError = result.IsError,
                    Message = result.Message
                };
                if (parseResult.IsError)
                    return StatusCode(400, parseResult);

                return StatusCode(200, controller.CreateYamlContentTemplate());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerError500Response(ex));
            }
        }

        /// <summary>
        /// For debugging purposes you can parse the yaml rule without executing it.
        /// </summary>
        /// <param name="yaml">The url pointing to the rule yaml</param>
        /// <returns>ParesResult</returns>
        /// <response code="200">Parsed</response>
        /// <response code="404">Yaml rule set could not be found</response>
        /// <response code="500">Server error</response>
        [HttpPost("validate-rule")]
        [ProducesResponseType(typeof(v1.Dto.ParseResult), 200)]
        [ProducesResponseType(typeof(ConfigurationInvalidResponse), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> ValidateRuleYaml(Uri url)
        {
            try
            {
                YamlScriptController controller = new YamlScriptController();
                string yaml = null;
                using (var client = new WebClient())
                {
                    try
                    {
                        yaml = client.DownloadString(url);
                    }
                    catch (WebException ex)
                    {
                        return StatusCode(404, ex.Message);
                    }
                }

                var result = controller.Parse(yaml);
                ParseResult parseResult = new ParseResult()
                {
                    IsError = result.IsError,
                    Message = result.Message
                };
                return StatusCode(200, parseResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerError500Response(ex));
            }
        }

        /// <summary>
        /// Executes a yaml rule file from a given uri.
        /// </summary>
        /// <param name="yaml">The url pointing to the rule yaml</param>
        /// <returns>ParesResult</returns>
        /// <response code="200">Executed</response>
        /// <response code="400">Yaml rule set contains errors</response>
        /// <response code="404">Yaml rule uri endpoint does not contain any rules</response>
        /// <response code="500">Server error</response>
        [HttpPost("execute-rule")]
        [ProducesResponseType(typeof(ExecuteRuleYamlFromUriResponse), 200)]
        [ProducesResponseType(typeof(ExecuteRuleYamlFromUriResponse), 400)]
        [ProducesResponseType(typeof(ExecuteRuleYamlFromUriResponse), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> ExecuteRuleYaml([FromBody] ExecuteRuleYamlFromUriRequest request)
        {
            return StatusCode(500, new ServerError500Response(new NotImplementedException()));

            var controller = new YamlScriptController();
            var response = new ExecuteRuleYamlFromUriResponse();
            var parameters = request.ClientParameters.Adapt<IParametersCollection>();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {

            };

            var downloadResult = request.Endpoint.DownloadYaml();
            var result = controller.Parse(downloadResult.Content);

            // map the parsing result.
            

            if (result.IsError)
            {
                return StatusCode(400, response);
            }
;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            
            return StatusCode(200, new ExecuteRuleYamlFromUriResponse());
        }

        /// <summary>
        /// Executes a yaml rule file from a given uri.
        /// </summary>
        /// <param name="yaml">The url pointing to the rule yaml</param>
        /// <returns>ParesResult</returns>
        /// <response code="200">Executed</response>
        /// <response code="400">Yaml rule set contains errors</response>
        /// <response code="404">Yaml rule set does not contain any rules</response>
        /// <response code="500">Server error</response>
        [HttpPost("execute-rule-from-contents")]
        [ProducesResponseType(typeof(ExecuteRuleYamlFromContentResponse), 200)]
        [ProducesResponseType(typeof(ExecuteRuleYamlFromUriResponse), 400)]
        [ProducesResponseType(typeof(ExecuteRuleYamlFromContentResponse), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> ExecuteRuleYamlContents(string yaml)
        {
            return StatusCode(500, new ServerError500Response(new NotImplementedException()));
        }
    }
}
