using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class Constraint : IConstraintSchema
    {
        private string _name;

        public string Name => _name;


        public Constraint()
        {

        }

        public Constraint(string name)
        {
            _name = name;
        }

        private class DeserializeTemplate
        {
            public string Name;
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (DeserializeTemplate)nestedObjectDeserializer(typeof(DeserializeTemplate));
            _name = o.Name;
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new { Name });
        }
    }
}