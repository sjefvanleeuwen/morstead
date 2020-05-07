using Semver;
using System;
using System.Collections.Generic;
using Vs.Core.Diagnostics;
using Vs.Core.Layers.Helpers;
using Vs.Core.Layers.Model.Interfaces;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Core.Layers.Model
{
    /// <summary>
    /// Contains the layer configuration for Virtual Society Systems
    /// </summary>
    public class LayerConfiguration : ILayerConfiguration, IYamlConvertible, Interfaces.IDebugInfo
    {
        /// <summary>
        /// Gets or sets the semantic version.
        /// </summary>
        /// <value>
        /// The semantic version.
        /// </value>
        public SemVersion Version { get; set; }
        /// <summary>
        /// Gets or sets the layers that define the system, indicated by name
        /// </summary>
        /// <value>
        /// The layers.
        /// </value>
        public IEnumerable<ILayer> Layers { get; set; }
        
        public DebugInfo DebugInfo { get; set; }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (TransformTemplate)nestedObjectDeserializer(typeof(TransformTemplate));
            if (o == null)
            {
                return;
            }
            DebugInfo = new DebugInfo().MapDebugInfo(parser.Current.Start, parser.Current.End);
            Version = o.version;
            var layers = new List<ILayer>();
            foreach (var layer in o.layers)
            {
                layers.Add(new Layer
                {
                    Name = layer.Key,
                    Contexts = layer.Value
                });
            }
            Layers = layers;
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new TransformTemplate(this));
        }

#pragma warning disable IDE1006 // Naming Styles
        private class TransformTemplate
        {
            public string version { get; set; }

            public Dictionary<string, IEnumerable<Context>> layers { get; set; } = new Dictionary<string, IEnumerable<Context>>();

            public TransformTemplate()
            {

            }

            public TransformTemplate(ILayerConfiguration local)
            {
                version = local.Version.ToString();
                foreach(var layer in local.Layers)
                {
                    layers.Add(layer.Name, (IEnumerable<Context>)layer.Contexts);
                }
            }
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
