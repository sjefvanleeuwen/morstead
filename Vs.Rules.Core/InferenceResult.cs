using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vs.Rules.Core
{
    public static partial class TypeInference
    {
        public class InferenceResult
        {
            public TypeEnum Type { get; }
            public object Value { get; }

            public InferenceResult(TypeEnum type, object value)
            {
                Type = type;
                Value = value;
            }

            [JsonConverter(typeof(StringEnumConverter))]
            [DataContract]
            public enum TypeEnum
            {
                [EnumMember(Value = "unknown")]
                Unknown,
                [EnumMember(Value = "double")]
                Double,
                [EnumMember(Value = "timespan")]
                TimeSpan,
                [EnumMember(Value = "datetime")]
                DateTime,
                [EnumMember(Value = "string")]
                String,
                [EnumMember(Value = "boolean")]
                Boolean,
                [EnumMember(Value = "list")]
                List,
                [EnumMember(Value = "period")]
                Period,
                [EnumMember(Value = "step")]
                Step
            }
        }
    }
}
