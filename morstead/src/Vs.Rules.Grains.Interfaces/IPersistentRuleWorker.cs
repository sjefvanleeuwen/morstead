using System.Threading.Tasks;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Grains.Interfaces
{
    public interface IPersistentRuleWorker : Orleans.IGrainWithStringKey
    {
        Task<IExecutionResult> Execute(string yaml, IParametersCollection parameters);
        Task<Model> Parse(string yaml);
        Task<ExecutionResult> GetState();
    }
}
