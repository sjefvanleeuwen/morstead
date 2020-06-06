using Orleans;
using System.Threading.Tasks;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;

namespace Vs.Morstead.Grains.Interfaces
{
    public interface IPersistentRuleWorker : IGrainWithStringKey
    {
        Task<ExecutionResult> Execute(string yaml, IParametersCollection parameters);
        Task<Model> Parse(string yaml);
        Task<IParametersCollection> GetState();
    }
}
