﻿using System;
using Vs.Core.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data.AttributeTypes
{
    [AttributeType("int")]
    public class AttributeInt : IAttributeType, IYamlConvertible
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
