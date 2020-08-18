using System.Threading.Tasks;
using Vs.Rules.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces
{
    public interface IServiceController
    {
        Task<IExecutionResult> Execute(IExecuteRequest executeRequest);
        Task GetExtraParameters(IEvaluateFormulaWithoutQARequest evaluateFormulaWithoutQARequest);
        Task<IParseResult> Parse(IParseRequest parseRequest);
    }
}