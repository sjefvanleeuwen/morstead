/*
 * Generated file using: https://app.quicktype.io/#l=cs&r=json2csharp
 * 
 * 
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.WebVowl
{
    public partial class Filter
    {
        [JsonProperty("checkBox")]
        public List<CheckBox> CheckBox { get; set; }

        [JsonProperty("degreeSliderValue")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long DegreeSliderValue { get; set; }
    }
}
