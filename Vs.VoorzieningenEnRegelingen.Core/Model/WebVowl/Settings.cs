/*
 * Generated file using: https://app.quicktype.io/#l=cs&r=json2csharp
 * 
 * 
 */

using Newtonsoft.Json;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.WebVowl
{
    public partial class Settings
    {
        [JsonProperty("global")]
        public Global Global { get; set; }

        [JsonProperty("gravity")]
        public Gravity Gravity { get; set; }

        [JsonProperty("filter")]
        public Filter Filter { get; set; }

        [JsonProperty("modes")]
        public Modes Modes { get; set; }
    }
}
