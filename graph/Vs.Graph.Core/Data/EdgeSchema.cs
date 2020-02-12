using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class EdgeSchema : IEdgeSchema
    {
        private string _name;

        public string Name => _name;

        public Constraints Constraints => _constraints;

        public Attributes Attributes => _attributes;

        private INodeSchemaScript _scriptProvider;
        private Constraints _constraints;
        private Attributes _attributes;

        public EdgeSchema(string name)
        {
            _name = name;
            _constraints = new Constraints();
            _attributes = new Attributes();
        }

        public EdgeSchema() { }

        private class DeserializeTemplate
        {
            public string Name;
            public Constraints Constraints;
            public Attributes Attributes;
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (DeserializeTemplate)nestedObjectDeserializer(typeof(DeserializeTemplate));
            _name = o.Name;
            _constraints = o.Constraints;
            _attributes = o.Attributes;
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new { Name, Constraints, Attributes });
        }
    }
}
