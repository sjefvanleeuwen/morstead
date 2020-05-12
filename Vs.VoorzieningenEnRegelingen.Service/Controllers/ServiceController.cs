using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Vs.Rules.Core;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;
using Vs.Rules.Routing.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase, IServiceController
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IYamlScriptController _yamlScriptController;
        private readonly IRoutingController _routingController;

        public ServiceController(ILogger<ServiceController> logger, IYamlScriptController yamlScriptController, IRoutingController routingController)
        {
            _logger = logger;
            _yamlScriptController = yamlScriptController;
            _routingController = routingController;
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
            return _yamlScriptController.Parse(parseRequest.Config);
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
            _yamlScriptController.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };

            var result = _yamlScriptController.Parse(executeRequest.Config);
            if (result.IsError)
            {
                return executionResult;
            }
            try
            {
                _yamlScriptController.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                //a paramater is yet unresolved
                ResolveQuestionFromRouting(ref executionResult, ref executeRequest);
            }
            return executionResult;
        }

        private void ResolveQuestionFromRouting(ref IExecutionResult executionResult, ref IExecuteRequest executeRequest)
        {
            try
            {
                var missingParameterNames = executionResult.Questions?.Parameters.Select(p => p.Name);
                foreach (string missingParameterName in missingParameterNames)
                {
                    if (MissingParameterHasRouting(missingParameterName))
                    {
                        var value = Task.Run(async () => await _routingController.GetParameterValue(missingParameterName)).Result;
                        if (value == null)
                        {
                            continue;
                        }
                        var parameter = new ClientParameter(missingParameterName, value, TypeInference.InferenceResult.TypeEnum.Unknown, missingParameterName);
                        executeRequest.Parameters.Add(parameter);
                        executionResult = Execute(executeRequest);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private bool MissingParameterHasRouting(string missingParameterName)
        {
            if (_routingController?.RoutingConfiguration == null)
            {
                return false;
            }

            return _routingController.RoutingConfiguration.Parameters.Any(p => p.Name == missingParameterName);
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

            _yamlScriptController.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
            };

            var result = _yamlScriptController.Parse(evaluateFormulaWithoutQARequest.Config);
            if (result.IsError)
            {
            }
            try
            {
                _yamlScriptController.EvaluateFormulaWithoutQA(ref parameters, evaluateFormulaWithoutQARequest.UnresolvedParameters);
            }
            catch (UnresolvedException)
            {
            }
        }
    }
}
