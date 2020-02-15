using System;
using System.Collections.Generic;
using Vs.Core.Diagnostics;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class Attributes : List<Attribute>, ISerialize, IDebugInfo
    {
        private class DeserializeTemplate : List<Attribute> { }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            AddRange((DeserializeTemplate) nestedObjectDeserializer(typeof(DeserializeTemplate)));
            DebugInfo = DebugInfo.MapDebugInfo(parser.Current.Start, parser.Current.End);
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new List<Attribute>(this));
        }

        public DebugInfo DebugInfo { get; set; }
    }
}
