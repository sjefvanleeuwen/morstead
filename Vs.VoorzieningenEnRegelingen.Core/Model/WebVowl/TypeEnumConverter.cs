/*
 * Generated file using: https://app.quicktype.io/#l=cs&r=json2csharp
 * 
 * 
 */

using Newtonsoft.Json;
using System;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.WebVowl
{
    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "owl:Class":
                    return TypeEnum.OwlClass;
                case "owl:DatatypeProperty":
                    return TypeEnum.OwlDatatypeProperty;
                case "owl:ObjectProperty":
                    return TypeEnum.OwlObjectProperty;
                case "rdfs:Literal":
                    return TypeEnum.RdfsLiteral;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.OwlClass:
                    serializer.Serialize(writer, "owl:Class");
                    return;
                case TypeEnum.OwlDatatypeProperty:
                    serializer.Serialize(writer, "owl:DatatypeProperty");
                    return;
                case TypeEnum.OwlObjectProperty:
                    serializer.Serialize(writer, "owl:ObjectProperty");
                    return;
                case TypeEnum.RdfsLiteral:
                    serializer.Serialize(writer, "rdfs:Literal");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}
