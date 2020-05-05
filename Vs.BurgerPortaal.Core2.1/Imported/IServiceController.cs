using Microsoft.AspNetCore.Mvc;
using Vs.Rules.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces
{
    public interface IServiceController
    {
        IExecutionResult Execute([FromBody] IExecuteRequest executeRequest);
        void GetExtraParameters([FromBody] IEvaluateFormulaWithoutQARequest evaluateFormulaWithoutQARequest);
        IParseResult Parse([FromBody] IParseRequest parseRequest);
    }
}