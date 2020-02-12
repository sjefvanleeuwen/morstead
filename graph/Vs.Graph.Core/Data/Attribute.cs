using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public class Attribute : IAttribute, ISerialize
    {
        private IAttributeType _type;
        private string _name;

        public IAttributeType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Attribute()
        {

        }

        public Attribute(string name, IAttributeType type)
        {
            _name = name;
            _type = type;
        }

        private class DeserializeTemplate
        {
            private string _name;

            public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                }
            }

            private string _type;

            public string Type
            {
                get
                {
                    return _type;
                }
                set
                {
                    _type = value;
                }
            }
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var o = (DeserializeTemplate)nestedObjectDeserializer(typeof(DeserializeTemplate));
            _name = o.Name;
            // Convert to correct IAttribute implementation from the serialization template
            var type = typeof(IAttributeType);
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p)))
            {
                var s =item.GetCustomAttributes(typeof(AttributeTypeAttribute), true);
                if (s.Length == 1)
                {
                    if (o.Type.ToLower() == ((AttributeTypeAttribute)s[0]).Name.ToLower())
                    {
                        _type = (IAttributeType)Activator.CreateInstance(item.UnderlyingSystemType);
                    }
                }
            }
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            nestedObjectSerializer(new { Name, Type});
        }
    }
}
