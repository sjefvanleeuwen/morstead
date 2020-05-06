using System.Collections.Generic;

namespace Vs.Core.Layers.Model
{
    /// <summary>
    /// A single layer within the system.
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// Gets or sets the name of the layer indicating the type of service, for example:
        /// rules, uxcontent, routing
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the contexts where the layer is configured.
        /// </summary>
        /// <value>
        /// The contexts.
        /// </value>
        public IEnumerable<Context> Contexts { get; set; }
    }
}