using Nager.Country;
using System;

namespace Vs.Core.Layers.Model
{
    /// <summary>
    /// Context of the layer configuration
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Gets or sets endpoint that contains the configuration of this context.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public Uri Endpoint { get; set; }
        /// <summary>
        /// Gets or sets the two letter language of which this context belongs to.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public LanguageCode Language { get; set; }
    }
}