using System;
using Vs.Core.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data.AttributeTypes
{
    [AttributeType("datum")]
    public class AttributeDatum : IAttributeType, IYamlConvertible
    {
        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            throw new NotImplementedException();
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer("datum");
        }
    }
}
