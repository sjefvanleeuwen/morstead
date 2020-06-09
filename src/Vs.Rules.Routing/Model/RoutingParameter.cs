using System;
using Vs.Core.Diagnostics;
using Vs.Core.Layers.Helpers;
using Vs.Rules.Routing.Model.Interfaces;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Rules.Routing.Model
{
    public class RoutingParameter : IRoutingParameter, IYamlConvertible, IDebugInfo
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Location
        /// </summary>
        /// <value>
        /// The location
        /// </value>
        public string Location { get; set; }

        private DebugInfo DebugInfo { get; set; }

        public LineInfo End => DebugInfo.End;

        public LineInfo Start => DebugInfo.Start;

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (TransformTemplate)nestedObjectDeserializer(typeof(TransformTemplate));
            if (o == null)
            {
                return;
            }
            DebugInfo = new DebugInfo().MapDebugInfo(parser.Current.Start, parser.Current.End);

            Name = o.waarde;
            Location = o.locatie;
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new TransformTemplate(this));
        }

#pragma warning disable IDE1006 // Naming Styles
        private class TransformTemplate
        {
            public string waarde { get; set; }
            public string locatie { get; set; }

            public TransformTemplate()
            {
            }

            public TransformTemplate(IRoutingParameter local)
            {
                waarde = local.Name;
                locatie = local.Location;
            }
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}