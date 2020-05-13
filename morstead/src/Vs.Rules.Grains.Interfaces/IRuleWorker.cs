using System.Threading.Tasks;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Grains.Interfaces
{
    public interface IRuleWorker : Orleans.IGrainWithIntegerKey
    {
        Task<IExecutionResult> Execute(string yaml, IParametersCollection parameters);
        Task<Model> Parse(string yaml);
    }
}
