using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class Attributes : List<Attribute>, ISerialize
    {
        public Attributes()
        {

        }

        private class DeserializeTemplate : List<Attribute> { }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)=> 
            AddRange((DeserializeTemplate) nestedObjectDeserializer(typeof(DeserializeTemplate)));

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            throw new NotImplementedException();
        }
    }
}
