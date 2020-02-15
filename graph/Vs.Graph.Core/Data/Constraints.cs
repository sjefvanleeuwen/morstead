using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class Constraints : List<Constraint>, ISerialize
    {
        private class DeserializeTemplate : List<Constraint> { }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)=>
            AddRange((DeserializeTemplate) nestedObjectDeserializer(typeof(DeserializeTemplate)));

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new List<Constraint>(this));
        }
    }
}