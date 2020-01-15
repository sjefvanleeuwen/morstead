/*
 * Generated file using: https://app.quicktype.io/#l=cs&r=json2csharp
 * 
 * 
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.WebVowl
{
    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, Converter.Settings);

        [JsonProperty("_comment")]
        public string Comment { get; set; }

        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("namespace")]
        public List<object> Namespace { get; set; }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }

        [JsonProperty("class")]
        public List<Class> Class { get; set; }

        [JsonProperty("classAttribute")]
        public List<Attribute> ClassAttribute { get; set; }

        [JsonProperty("property")]
        public List<Class> Property { get; set; }

        [JsonProperty("propertyAttribute")]
        public List<Attribute> PropertyAttribute { get; set; }
    }


}
