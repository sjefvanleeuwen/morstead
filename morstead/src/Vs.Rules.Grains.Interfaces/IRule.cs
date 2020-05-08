using System.Threading.Tasks;
using Vs.Rules.Core.Interfaces;

namespace Vs.Rules.Grains.Interfaces
{
    public interface IRuleWorker : Orleans.IGrainWithIntegerKey
    {
        Task<IExecutionResult> Execute(string yaml, IParametersCollection parameters);
    }
}
