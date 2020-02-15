using System;
using Vs.Core.Diagnostics;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class NodeSchema : INodeSchema, IDebugInfo
    {
        public string Name { get; set; }
        public Attributes Attributes { get; set; }
        public Edges Edges { get; set; }
        public DebugInfo DebugInfo { get; set; }

        private INodeSchemaScript _scriptProvider;

        public NodeSchema() { }

        public NodeSchema(string name)
        {
            Name = name;
            Attributes = new Attributes();
            Edges = new Edges();
        }

        private class DeserializeTemplate
        {
            public string Name { get; set; }
            public Attributes Attributes { get; set; }
            public Edges Edges { get; set; }
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (DeserializeTemplate)nestedObjectDeserializer(typeof(DeserializeTemplate));
            Name = o.Name;
            Attributes = o.Attributes;
            Edges = o.Edges;
            DebugInfo = DebugInfo.MapDebugInfo(parser.Current.Start, parser.Current.End);
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new { Global.Version, Name, Attributes, Edges });
        }
    }
}
