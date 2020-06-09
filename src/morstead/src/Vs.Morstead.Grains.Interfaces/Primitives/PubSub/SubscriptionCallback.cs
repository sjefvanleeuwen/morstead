using System;

namespace Vs.Morstead.Grains.Interfaces.Primitives.PubSub
{
    public class SubscriptionCallback
    {
        public string Topic { get; set; }
        public Type Interface { get; set; }
        public string GrainId { get; set; }
    }
}
