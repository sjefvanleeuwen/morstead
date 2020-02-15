﻿using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class EdgeSchema : IEdgeSchema
    {
        public string Name { get; set; }
        public Constraints Constraints { get; set; }
        public Attributes Attributes { get; set; }

        private INodeSchemaScript _scriptProvider;

        public EdgeSchema(string name)
        {
            Name = name;
            Constraints = new Constraints();
            Attributes = new Attributes();
        }

        public EdgeSchema()
        {
            Constraints = new Constraints();
            Attributes = new Attributes();
        }

        public class DeserializeTemplate
        {
            public string Name { get; set; }
            public Constraints Constraints { get; set; }
            public Attributes Attributes { get; set; }
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (DeserializeTemplate)nestedObjectDeserializer(typeof(DeserializeTemplate));
            Name = o.Name;
            Constraints = o.Constraints;
            Attributes = o.Attributes;
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new { Name, Constraints, Attributes });
        }
    }
}