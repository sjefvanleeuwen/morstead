/*
 * Generated file using: https://app.quicktype.io/#l=cs&r=json2csharp
 * 
 * 
 */

using Newtonsoft.Json;
using System;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.WebVowl
{
    internal class AttributeElementConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AttributeElement) || t == typeof(AttributeElement?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "datatype":
                    return AttributeElement.Datatype;
                case "object":
                    return AttributeElement.Object;
            }
            throw new Exception("Cannot unmarshal type AttributeElement");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AttributeElement)untypedValue;
            switch (value)
            {
                case AttributeElement.Datatype:
                    serializer.Serialize(writer, "datatype");
                    return;
                case AttributeElement.Object:
                    serializer.Serialize(writer, "object");
                    return;
            }
            throw new Exception("Cannot marshal type AttributeElement");
        }

        public static readonly AttributeElementConverter Singleton = new AttributeElementConverter();
    }
}
