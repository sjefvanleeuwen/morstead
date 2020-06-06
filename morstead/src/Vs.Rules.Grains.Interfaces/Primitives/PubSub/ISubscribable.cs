using System.Threading.Tasks;

namespace Vs.Rules.Grains.Interfaces.Primitives.PubSub
{
    public interface ISubscribableGrain
    {
        Task<IPubSubGrain> GetPubSub();
    }
}
