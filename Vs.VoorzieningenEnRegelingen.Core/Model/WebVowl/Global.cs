/*
 * Generated file using: https://app.quicktype.io/#l=cs&r=json2csharp
 * 
 * 
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.WebVowl
{
    public partial class Global
    {
        [JsonProperty("zoom")]
        public string Zoom { get; set; }

        [JsonProperty("translation")]
        public List<double> Translation { get; set; }

        [JsonProperty("paused")]
        public bool Paused { get; set; }
    }
}
