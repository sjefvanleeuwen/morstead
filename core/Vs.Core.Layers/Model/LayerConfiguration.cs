using Semver;
using System.Collections.Generic;
using Vs.Core.Layers.Model.Interfaces;

namespace Vs.Core.Layers.Model
{
    /// <summary>
    /// Contains the layer configuration for Virtual Society Systems
    /// </summary>
    public class LayerConfiguration : ILayerConfiguration
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
        public IEnumerable<ILayer> Layers { get; set; }
    }
}
