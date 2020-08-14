using Microsoft.AspNetCore.Mvc;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces
{
    public interface IAPIServiceController
    {
        IExecutionResult Execute([FromBody] IExecuteRequest executeRequest);
        void GetExtraParameters([FromBody] IEvaluateFormulaWithoutQARequest evaluateFormulaWithoutQARequest);
        IParseResult Parse([FromBody] IParseRequest parseRequest);
    }
}