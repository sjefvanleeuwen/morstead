using Vs.Rules.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces
{
    public interface IServiceController
    {
        IExecutionResult Execute(IExecuteRequest executeRequest);
        void GetExtraParameters(IEvaluateFormulaWithoutQARequest evaluateFormulaWithoutQARequest);
        IParseResult Parse(IParseRequest parseRequest);
    }
}