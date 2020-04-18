using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using Vs.Rules.Core;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces;

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
        public IParseResult Parse([FromBody] IParseRequest parseRequest)
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
        public IExecutionResult Execute([FromBody] IExecuteRequest executeRequest)
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
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            executeRequest.Config = parseHelper(executeRequest.Config);
            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };

            var result = controller.Parse(executeRequest.Config);
            if (result.IsError)
            {
                return executionResult;
            }
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
            }
            return executionResult;
        }

        [HttpPost("EvaluateFormulaWithoutQA")]
        public void GetExtraParameters([FromBody] IEvaluateFormulaWithoutQARequest evaluateFormulaWithoutQARequest)
        {
            if (evaluateFormulaWithoutQARequest is null)
            {
                throw new ArgumentNullException(nameof(evaluateFormulaWithoutQARequest));
            }
            if (evaluateFormulaWithoutQARequest.Parameters == null)
            {
                evaluateFormulaWithoutQARequest.Parameters = new ParametersCollection();
            }
            if (evaluateFormulaWithoutQARequest.UnresolvedParameters == null || !evaluateFormulaWithoutQARequest.UnresolvedParameters.Any())
            {
                return;
            }

            var parameters = evaluateFormulaWithoutQARequest.Parameters;
            evaluateFormulaWithoutQARequest.Config = parseHelper(evaluateFormulaWithoutQARequest.Config);

            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
            };

            var result = controller.Parse(evaluateFormulaWithoutQARequest.Config);
            if (result.IsError)
            {
            }
            try
            {
                controller.EvaluateFormulaWithoutQA(ref parameters, evaluateFormulaWithoutQARequest.UnresolvedParameters);
            }
            catch (UnresolvedException)
            {
            }
        }
    }
}
