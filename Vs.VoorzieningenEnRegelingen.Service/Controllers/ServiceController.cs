using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Net;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase, IServiceController
    {
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
        }

        static ConcurrentDictionary<string, string> UrlContentCache = new ConcurrentDictionary<string, string>();

        private static string parseHelper(string config)
        {
            if (config.StartsWith("http"))
            {
                if (UrlContentCache.ContainsKey(config))
                {
                    return UrlContentCache[config];
                }
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
        public ExecutionResult Execute([FromBody] ExecuteRequest executeRequest)
        {
            if (executeRequest is null)
            {
                throw new ArgumentNullException(nameof(executeRequest));
            }
            if (executeRequest.Parameters == null)
            {
                executeRequest.Parameters = new ParametersCollection();
            }
            var parameters = executeRequest.Parameters;
            ExecutionResult executionResult = new ExecutionResult(ref parameters);
            executeRequest.Config = parseHelper(executeRequest.Config);
            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };

            var result = controller.Parse(executeRequest.Config);
            if (result.IsError)
                return executionResult;
            try
            {

                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
            }
            return executionResult;
        }

        private void Execute_QuestionCallback(FormulaExpressionContext sender, QuestionArgs args)
        {
            // throw new NotImplementedException();
        }
    }
}
