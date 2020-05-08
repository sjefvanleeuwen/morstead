using Nager.Country;
using System;
using Vs.Core.Diagnostics;
using Vs.Core.Layers.Helpers;
using Vs.Core.Layers.Model.Interfaces;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Core.Layers.Model
{
    /// <summary>
    /// Context of the layer configuration
    /// </summary>
    public class Context : IContext, IYamlConvertible, Interfaces.IDebugInfo
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
        public LanguageCode? Language { get; set; }
        
        public DebugInfo DebugInfo { get; set; }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (TransformTemplate)nestedObjectDeserializer(typeof(TransformTemplate));
            DebugInfo = new DebugInfo().MapDebugInfo(parser.Current.Start, parser.Current.End);
            Endpoint = !string.IsNullOrWhiteSpace(o.context) ? new Uri(o.context) : null;
            Language = !string.IsNullOrWhiteSpace(o.language) ? (LanguageCode?)Enum.Parse(typeof(LanguageCode), o.language.ToString(), true) : null;
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new TransformTemplate(this));
        }

#pragma warning disable IDE1006 // Naming Styles
        private class TransformTemplate
        {
            public string context { get; set; }
            public string language { get; set; }

            public TransformTemplate()
            {

            }

            public TransformTemplate(IContext local)
            {
                context = local.Endpoint.ToString();
                language = local.Language?.ToString();
            }
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}