using Semver;
using System.Collections.Generic;

namespace Vs.Core.Layers.Model
{
    /// <summary>
    /// Contains the layer configuration for Virtual Society Systems
    /// </summary>
    public class LayerConfiguration
    {
        /// <summary>
        /// Gets or sets the semantic version.
        /// </summary>
        /// <value>
        /// The semantic version.
        /// </value>
        public SemVersion Version { get; set; }
        /// <summary>
        /// Gets or sets the layers that define the system.
        /// </summary>
        /// <value>
        /// The layers.
        /// </value>
        public IEnumerable<Layer> Layers {get;set;}
    }
}
