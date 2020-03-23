using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using static Vs.VoorzieningenEnRegelingen.Core.TypeInference.InferenceResult;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class ClientParameter : IClientParameter
    {
        public string Name { get; set; }

        private object _value;

        public ClientParameter() { }

        public ClientParameter(string name, object value, TypeEnum? type = null)
        {
            Name = name;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (value == null && type == TypeEnum.Double)
            {
                _value = double.Parse("0");
                Type = type.Value;
                return;
            }

            if (value == null && type == TypeEnum.Boolean)
            {
                _value = false;
                Type = type.Value;
                return;
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _value = value.Infer();
            Type = type ?? TypeInference.Infer(value.ToString()).Type;
        }

        [JsonIgnore()]
        public string ValueAsString
        {
            get
            {
                return _value.ToString();
            }
            set
            {
                Value = value.Infer();
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value.Infer();
                if (Type == null)
                    Type = TypeInference.Infer(value.ToString()).Type;
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public TypeEnum Type { get; set; }

        public string TypeAsString
        {
            get
            {
                return Type.ToString();
            }
        }

        public bool IsCalculated { get; internal set; }
        public string SemanticKey { get; set; }
    }
}

