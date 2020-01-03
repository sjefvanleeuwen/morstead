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

        [HttpPost("parse")]
        public ParseResult Parse([FromBody] ParseRequest parseRequest)
        {
            var controller = new YamlScriptController();
            if (parseRequest.Config.StartsWith("http")){
                using (var client = new WebClient())
                {
                    parseRequest.Config = client.DownloadString(parseRequest.Config);
                }
            }
            return controller.Parse(parseRequest.Config);
        }

        [HttpPost("execute")]
        public Tuple<ParseResult,ExecutionResult> Execute(string config, ParametersCollection parameters)
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(config);
            controller.QuestionCallback = Execute_QuestionCallback;
            if (result.IsError)
                return new Tuple<ParseResult, ExecutionResult>(result, ExecutionResult.NotExecutedBecauseOfParseError);
            var executionResult = controller.ExecuteWorkflow(ref parameters);
            return new Tuple<ParseResult, ExecutionResult>(result, executionResult);
        }

        private void Execute_QuestionCallback(FormulaExpressionContext sender, QuestionArgs args)
        {
           // throw new NotImplementedException();
        }
    }
}
