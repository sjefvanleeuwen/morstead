using Orleans;
using System;
using System.Threading.Tasks;

namespace Vs.Rules.Grains.Interfaces.Primitives.PubSub
{
    public interface IPubSubGrain : IGrainWithStringKey, ISubscribeGrain
    {
        Task Notify(string topic);
        Task SetPublishingGrain(Type _interface, string grainId);
    }
}
