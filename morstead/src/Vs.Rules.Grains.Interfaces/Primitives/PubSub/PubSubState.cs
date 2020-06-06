using System;
using System.Collections.Generic;

namespace Vs.Rules.Grains.Interfaces.Primitives.PubSub
{
    public class PubSubState
    {
        public Dictionary<string,PubSubSubscriber> Subscriptions { get; set; }
        /// <summary>
        /// Gets or sets the publishing grain identifier.
        /// </summary>
        /// <value>
        /// The grain identifier.
        /// </value>
        public string GrainId { get; set; }
        /// <summary>
        /// Gets or sets the publishing grain interface type.
        /// </summary>
        /// <value>
        /// The interface.
        /// </value>
        public Type Interface { get; set; }
    }
}
