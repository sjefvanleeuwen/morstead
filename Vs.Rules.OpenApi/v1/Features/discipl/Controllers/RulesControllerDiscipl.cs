using Mapster;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Vs.Core.Web.OpenApi;
using Vs.Core.Web.OpenApi.v1.Dto.ProtocolErrors;
using Vs.Rules.Core;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.OpenApi.Helpers;
using Vs.Rules.OpenApi.v1.Dto;
using Vs.Rules.OpenApi.v1.Features.discipl.Dto;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ParseResult = Vs.Rules.OpenApi.v1.Dto.ParseResult;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

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
    public class RulesControllerDiscipl : VsControllerBase
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
        [Authorize(Roles = "ux-writer")]
        public async Task<IActionResult> GenerateContentTemplate(Uri url)
        {
            try
            {
                var ret = await Download(url);
                if (ret.StatusCode != 200)
                    return ret;

                var yaml = ret.Value.ToString();
                YamlScriptController controller = new YamlScriptController();
                var result = controller.Parse(yaml);
                ParseResult parseResult = new ParseResult()
                {

                };
                if (result.IsError)
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
        [Microsoft.AspNetCore.Mvc.HttpPost("validate-rule")]
        [ProducesResponseType(typeof(v1.Dto.ParseResult), 200)]
        [ProducesResponseType(typeof(ConfigurationInvalidResponse), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        [Authorize(Roles = "law-analist")]
        public async Task<IActionResult> ValidateRuleYaml(Uri url)
        {
            try
            {
                var ret = await Download(url);
                if (ret.StatusCode != 200)
                    return ret;
                YamlScriptController controller = new YamlScriptController();
                var yaml = ret.Value.ToString();
                var result = controller.Parse(yaml);
                ParseResult parseResult = new ParseResult()
                {
                };
                return StatusCode(200, parseResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServerError500Response(ex));
            }
        }

        /// <summary>
        /// Debugs a yaml rule by passing its content.
        /// </summary>
        /// <returns>ParesResult</returns>
        /// <response code="200">Debug information received.</response>
        /// <response code="404">The yaml rule set does not contain any errors.</response>
        /// <response code="500">Server error</response>
        [HttpPost("debug-rule-from-contents")]
        [ProducesResponseType(typeof(DebugRuleYamlFromContentResponse), 200)]
        [ProducesResponseType(typeof(DebugRuleYamlFromContentResponse), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> DebugRuleYamlContents([FromBody] DebugRuleYamlFromContentRequest request)
        {
            var response = new DebugRuleYamlFromContentResponse();
            var controller = new YamlScriptController();
            var result = controller.Parse(request.Yaml);
            if (result.IsError)
            {
                response.ParseResult = new ParseResult();
                response.ParseResult.FormattingExceptions = result.Exceptions.Exceptions.Adapt<List<Dto.Exceptions.FormattingException>>();
                response.ExecutionStatus = ExecuteRuleYamlResultTypes.SyntaxError;
                return StatusCode(200, response);
            }
            // No Errors found.
            return StatusCode(404, response);
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
            var controller = new YamlScriptController();
            var response = new ExecuteRuleYamlFromUriResponse();
            var parameters = request.ClientParameters.Adapt<IParametersCollection>();
            IExecutionResult executionResult = null;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };

            var downloadResult = request.Endpoint.DownloadYaml();
            var result = controller.Parse(downloadResult.Content);

            if (result.IsError)
            {
                return StatusCode(400, response);
            }
;
            executionResult = new ExecutionResult(ref parameters);
            try
            {
                executionResult = controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
            }

            if (executionResult.IsError)
            {
                response.ExecutionStatus = ExecuteRuleYamlResultTypes.SyntaxError;
                return StatusCode(400, response);
            }
            response.ServerParameters = executionResult.Parameters.Adapt<IEnumerable<ServerParameter>>();
            return StatusCode(200, new ExecuteRuleYamlFromUriResponse());
        }

        /// <summary>
        /// Executes a yaml rule using its content.
        /// </summary>
        /// <param name="yaml">The url pointing to the rule yaml</param>
        /// <returns>ParesResult</returns>
        /// <response code="200">Executed</response>
        /// <response code="400">Yaml rule set contains errors</response>
        /// <response code="404">Yaml rule set does not contain any rules</response>
        /// <response code="500">Server error</response>
        [HttpPost("execute-rule-from-contents")]
        [ProducesResponseType(typeof(ExecuteRuleYamlFromContentResponse), 200)]
        [ProducesResponseType(typeof(ExecuteRuleYamlFromContentResponse), 400)]
        [ProducesResponseType(typeof(ExecuteRuleYamlFromContentResponse), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> ExecuteRuleYamlContents([FromBody] ExecuteRuleYamlFromContentRequest request)
        {
            if (String.IsNullOrEmpty(request.Yaml))
                return StatusCode(400, null);
            var controller = new YamlScriptController();
            var response = new ExecuteRuleYamlFromContentResponse();
            var parameters = request.ClientParameters.Adapt<IParametersCollection>();
            IExecutionResult executionResult = null;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };
            var result = controller.Parse(request.Yaml);
            if (result.IsError)
            {
                response.ParseResult = new ParseResult();
                response.ParseResult.FormattingExceptions = result.Exceptions.Exceptions.Adapt<List<Dto.Exceptions.FormattingException>>();
                response.ExecutionStatus = ExecuteRuleYamlResultTypes.SyntaxError;
               
                return StatusCode(400, response);
            }
            executionResult = new ExecutionResult(ref parameters);
            try
            {
                executionResult = controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
            }
            if (executionResult.IsError)
            {
                response.ExecutionStatus = ExecuteRuleYamlResultTypes.Ok;
                return StatusCode(400, response);
            }
            response.ServerParameters = executionResult.Parameters.Adapt<IEnumerable<ServerParameter>>();
            return StatusCode(200, new ExecuteRuleYamlFromContentResponse());
        }
    }
}
