using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces
{
    public interface IAPIServiceController
    {
        Task<IExecutionResult> Execute([FromBody] IExecuteRequest executeRequest);
        Task GetExtraParameters([FromBody] IEvaluateFormulaWithoutQARequest evaluateFormulaWithoutQARequest);
        Task<IParseResult> Parse([FromBody] IParseRequest parseRequest);
    }
}