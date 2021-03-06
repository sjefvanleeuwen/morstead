﻿using System;
using Vs.Core.Diagnostics;
using Vs.Graph.Core.Helpers;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class Constraint : IConstraintSchema, IDebugInfo
    {
        public string Name { get; set; }

        public Constraint()
        {

        }

        public Constraint(string name)
        {
            Name = name;
        }

        private class DeserializeTemplate
        {
            public string Name { get; set; }
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (DeserializeTemplate)nestedObjectDeserializer(typeof(DeserializeTemplate));
            Name = o.Name;
            DebugInfo = new DebugInfo().MapDebugInfo(parser.Current.Start, parser.Current.End);
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new { Name });
        }

        public DebugInfo DebugInfo { get; set; }
    }
}