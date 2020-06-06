using Orleans;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces.Primitives.PubSub
{
    public interface ISubscribeGrain : IGrainWithStringKey
    {
        Task Subscribe(PubSubSubscriber subscriber);
    }
}