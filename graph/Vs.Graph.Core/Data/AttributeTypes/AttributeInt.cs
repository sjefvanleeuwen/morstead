using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data.AttributeTypes
{
    [AttributeType("int")]
    public class AttributeInt : IAttributeType, ISerialize
    {
        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            throw new NotImplementedException();
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer("int");
        }
    }
}
