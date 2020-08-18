using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Routing.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIServiceController : ControllerBase, IAPIServiceController
    {
        private readonly IServiceController _serviceController;

        public APIServiceController(ILogger<ServiceController> logger, IYamlScriptController yamlScriptController, IRoutingController routingController)
        {
            _serviceController = new ServiceController(logger, yamlScriptController, routingController);
        }

        [HttpPost("parse")]
        public async Task<IParseResult> Parse([FromBody] IParseRequest parseRequest)
        {
            return await _serviceController.Parse(parseRequest);
        }

        [HttpPost("execute")]
        public async Task<IExecutionResult> Execute([FromBody] IExecuteRequest executeRequest)
        {
            return await _serviceController.Execute(executeRequest);
        }

        [HttpPost("EvaluateFormulaWithoutQA")]
        public async Task GetExtraParameters([FromBody] IEvaluateFormulaWithoutQARequest evaluateFormulaWithoutQARequest)
        {
            await _serviceController.GetExtraParameters(evaluateFormulaWithoutQARequest);
        }   
    }
}
