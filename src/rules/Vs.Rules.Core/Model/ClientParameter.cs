﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static Vs.Rules.Core.TypeInference.InferenceResult;

namespace Vs.Rules.Core.Model
{
    public class ClientParameter : IClientParameter
    {
        public string Name { get; set; }

        private object _value;

        public ClientParameter(string name, object value, TypeEnum type, string semanticKey)
        {
            Name = name;
            SemanticKey = semanticKey;
            Type = type;

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (value == null && type == TypeEnum.Double)
            {
                _value = 0d;
                return;
            }

            if (value == null && type == TypeEnum.Boolean)
            {
                _value = false;
                return;
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            //TODO no inference needed, the correct type is provided
            _value = value.Infer();
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

