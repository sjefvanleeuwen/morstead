using System.IO;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class SchemaController
    {
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
