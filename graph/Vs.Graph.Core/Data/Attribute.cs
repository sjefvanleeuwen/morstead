using System;
using System.Linq;
using System.Reflection;
using Vs.Core.Diagnostics;
using Vs.Core.Serialization;
using Vs.Graph.Core.Helpers;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class Attribute : IAttribute, ISerialize, IDebugInfo
    {
        public IAttributeType Type { get; set; }

        public string Name { get; set; }

        public Attribute()
        {

        }

        public Attribute(string name, IAttributeType type)
        {
            Name = name;
            Type = type;
        }

        private class DeserializeTemplate
        {
            public string Name { get; set; }

            public string Type { get; set; }
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (DeserializeTemplate)nestedObjectDeserializer(typeof(DeserializeTemplate));
            Name = o.Name;
            DebugInfo = new DebugInfo().MapDebugInfo(parser.Current.Start, parser.Current.End);
            // Convert to correct IAttribute implementation from the serialization template
            var type = typeof(IAttributeType);
            foreach (var item in Assembly.GetAssembly(typeof(AttributeTypeAttribute)).GetTypes()
                .Where(p => type.IsAssignableFrom(p)))
            {
                var s = item.GetCustomAttributes(typeof(AttributeTypeAttribute), true);
                if (s.Length == 1)
                {
                    if (o.Type.ToLower() == ((AttributeTypeAttribute)s[0]).Name.ToLower())
                    {
                        Type = (IAttributeType)Activator.CreateInstance(item.UnderlyingSystemType);
                    }
                }
            }
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new { Name, Type });
        }

        public DebugInfo DebugInfo { get; set; }
    }
}
