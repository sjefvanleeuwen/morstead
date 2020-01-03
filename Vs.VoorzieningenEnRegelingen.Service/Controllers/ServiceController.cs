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

        private static void parseHelper(ref ParseRequest parseRequest)
        {
            if (parseRequest.Config.StartsWith("http"))
            {
                using (var client = new WebClient())
                {
                    parseRequest.Config = client.DownloadString(parseRequest.Config);
                }
            }
        }

        [HttpPost("parse")]
        public ParseResult Parse([FromBody] ParseRequest parseRequest)
        {
            if (parseRequest is null)
            {
                throw new ArgumentNullException(nameof(parseRequest));
            }
            parseHelper(ref parseRequest);
            var controller = new YamlScriptController();
            return controller.Parse(parseRequest.Config);
        }

        [HttpPost("execute")]
        public Tuple<ParseResult,ExecutionResult> Execute([FromBody] ParseRequest parseRequest, [FromBody] ParametersCollection parameters)
        {
            parseHelper(ref parseRequest);
            var controller = new YamlScriptController();
            var result = controller.Parse(parseRequest.Config);
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
