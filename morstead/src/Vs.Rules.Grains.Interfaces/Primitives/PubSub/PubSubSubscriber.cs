using System;

namespace Vs.Rules.Grains.Interfaces.Primitives.PubSub
{
    public class PubSubSubscriber
    {
        /// <summary>
        /// Gets or sets the subscribing grain identifier.
        /// </summary>
        /// <value>
        /// The grain identifier.
        /// </value>
        public string GrainId { get; set; }
        /// <summary>
        /// Gets or sets the subscribing grain interface type.
        /// </summary>
        /// <value>
        /// The interface.
        /// </value>
        public Type Interface { get; set; }
        /// <summary>
        /// Gets or sets the grain callback method.
        /// </summary>
        /// <value>
        /// The grain ballback methoed.
        /// </value>
        public string GrainCallbackMethod { get; set; }
    }
}