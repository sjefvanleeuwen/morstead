using System.IO;
using Vs.Graph.Core.Data;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core
{
    public class SchemaController
    {
        public IGraphSchemaService Service { get; }

        public SchemaController(IGraphSchemaService service)
        {
            Service = service;
        }

        public string Serialize(NodeSchema schema)
        {
            var serializer = new SerializerBuilder().Build();
            var sw = new StringWriter();
            serializer.Serialize(sw, schema);
            return sw.ToString();
        }

        public NodeSchema Deserialize(string yaml)
        {
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            return deserializer.Deserialize<NodeSchema>(yaml);
        }
    }
}
