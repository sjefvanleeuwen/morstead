using Orleans;
using System.Threading.Tasks;

namespace Vs.Rules.Grains.Interfaces.Primitives.PubSub
{
    public interface ISubscribeGrain : IGrainWithStringKey
    {
        Task Subscribe(PubSubSubscriber subscriber);
    }
}