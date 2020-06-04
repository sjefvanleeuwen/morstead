using Orleans;
using System.Threading.Tasks;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Grains.Interfaces
{
    public interface IRuleWorker : IGrainWithIntegerKey
    {
        Task<IExecutionResult> Execute(string yaml, IParametersCollection parameters);
        Task<Model> Parse(string yaml);
    }
}
