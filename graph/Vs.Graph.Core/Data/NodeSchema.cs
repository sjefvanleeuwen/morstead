using System;
using System.Dynamic;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class NodeSchema : INodeSchema
    {
        private string _name;
        private Attributes _attributes;
        private Guid _objectId;

        public string Name => _name;

        public Attributes Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public Edges Edges => _edges;

        //public Guid ObjectId => _objectId;

        private INodeSchemaScript _scriptProvider;
        private Edges _edges;

        public NodeSchema() { }

        public NodeSchema(string name)
        {
            _name = name;
            _attributes = new Attributes();
            _edges = new Edges();
        }

        private class DeserializeTemplate
        {

            public string Name { 
                get;
                set;
            }
            public Attributes Attributes { get; set; }
            public Edges Edges { 
                get; 
                set;
            }
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (DeserializeTemplate)nestedObjectDeserializer(typeof(DeserializeTemplate));
            _name = o.Name;
            _attributes = o.Attributes;
            _edges = o.Edges;
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new { Global.Version, Name, Attributes, Edges });
        }
    }
}
