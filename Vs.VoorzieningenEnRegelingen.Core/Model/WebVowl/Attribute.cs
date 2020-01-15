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
    public partial class Attribute
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("iri")]
        public Uri Iri { get; set; }

        [JsonProperty("baseIri")]
        public Uri BaseIri { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("pos")]
        public List<double> Pos { get; set; }

        [JsonProperty("attributes", NullValueHandling = NullValueHandling.Ignore)]
        public List<AttributeElement> Attributes { get; set; }

        [JsonProperty("domain", NullValueHandling = NullValueHandling.Ignore)]
        public string Domain { get; set; }

        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public string Range { get; set; }
    }
}
