/*
 * Generated file using: https://app.quicktype.io/#l=cs&r=json2csharp
 * 
 * 
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.WebVowl
{
    public partial class Header
    {
        [JsonProperty("languages")]
        public List<string> Languages { get; set; }

        [JsonProperty("baseIris")]
        public List<Uri> BaseIris { get; set; }

        [JsonProperty("iri")]
        public Uri Iri { get; set; }

        [JsonProperty("title")]
        public Description Title { get; set; }

        [JsonProperty("description")]
        public Description Description { get; set; }
    }
}
