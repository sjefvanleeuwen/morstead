using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Vs.Rules.Grains.Interfaces.Primitives.PubSub;

namespace Vs.Rules.Grains.Primitives
{
    public class PubSubGrain : Grain, IPubSubGrain
    {
        private IPersistentState<PubSubState> _pubsub;

        private bool publishingGrainInitialized => _pubsub.State.Interface != null && !string.IsNullOrEmpty(_pubsub.State.GrainId);

        public PubSubGrain([PersistentState("pub-sub", "pub-sub-store")]
            IPersistentState<PubSubState> pubsub)
        {
            _pubsub = pubsub;
            
        }

        public override Task OnActivateAsync()
        {
            
            return base.OnActivateAsync();
        }

        public async Task SetPublishingGrain(Type @interface, string grainId)
        {
            if (publishingGrainInitialized)
                throw new Exception("Publishing grain can only be set once.");
            _pubsub.State.GrainId = grainId;
            _pubsub.State.Interface = @interface;
            await _pubsub.WriteStateAsync(); 
        }

        public async Task Notify(string topic)
        {
            if (_pubsub.State.Subscriptions.Count == 0)
                return;
            var callback = new SubscriptionCallback()
            {
                GrainId = _pubsub.State.GrainId,
                Topic = topic,
                Interface = _pubsub.State.Interface
            };
            foreach (var item in _pubsub.State.Subscriptions)
            {
                var subscriber = GrainFactory.GetGrain(item.Value.Interface, item.Key);
                MethodInfo theMethod = item.Value.Interface.GetMethod(item.Value.GrainCallbackMethod);
                theMethod.Invoke(subscriber, new[] { callback });
                await ((IPublish)subscriber).Notify(topic);
            }
        }

        public async Task Subscribe(PubSubSubscriber subscriber)
        {
            if (_pubsub.State.Subscriptions == null)
                _pubsub.State.Subscriptions = new Dictionary<string, PubSubSubscriber>();
            _pubsub.State.Subscriptions.Add(subscriber.GrainId, subscriber);
            await _pubsub.WriteStateAsync();
        }

    }
}
