using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces.Primitives.PubSub
{
    public interface ISubscribableGrain
    {
        Task<IPubSubGrain> GetPubSub();
    }
}
