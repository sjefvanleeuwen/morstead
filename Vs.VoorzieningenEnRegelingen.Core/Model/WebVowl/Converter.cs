/*
 * Generated file using: https://app.quicktype.io/#l=cs&r=json2csharp
 * 
 * 
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.WebVowl
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypeEnumConverter.Singleton,
                AttributeElementConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
