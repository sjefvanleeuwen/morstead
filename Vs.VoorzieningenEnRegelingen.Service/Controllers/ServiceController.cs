using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
        }

        private static string parseHelper(string config)
        {
            if (config.StartsWith("http"))
            {
                using (var client = new WebClient())
                {
                    return client.DownloadString(config);
                }
            }
            return config;
        }

        [HttpPost("parse")]
        public ParseResult Parse([FromBody] ParseRequest parseRequest)
        {
            if (parseRequest is null)
            {
                throw new ArgumentNullException(nameof(parseRequest));
            }
            parseRequest.Config = parseHelper(parseRequest.Config);
            var controller = new YamlScriptController();
            return controller.Parse(parseRequest.Config);
        }

        [HttpPost("execute")]
        public Tuple<ParseResult,ExecutionResult> Execute([FromBody] ExecuteRequest executeRequest)
        {
            if (executeRequest is null)
            {
                throw new ArgumentNullException(nameof(executeRequest));
            }
            if (executeRequest.Parameters == null)
            {
                executeRequest.Parameters = new ParametersCollection();
            }

            executeRequest.Config = parseHelper(executeRequest.Config);
            var controller = new YamlScriptController();
            var result = controller.Parse(executeRequest.Config);
            controller.QuestionCallback = Execute_QuestionCallback;
            if (result.IsError)
                return new Tuple<ParseResult, ExecutionResult>(result, ExecutionResult.NotExecutedBecauseOfParseError);
            var parameters = executeRequest.Parameters;
            var executionResult = controller.ExecuteWorkflow(ref parameters);
            return new Tuple<ParseResult, ExecutionResult>(result, executionResult);
        }

        private void Execute_QuestionCallback(FormulaExpressionContext sender, QuestionArgs args)
        {
           // throw new NotImplementedException();
        }
    }
}
