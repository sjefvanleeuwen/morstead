/*
 * Generated file using: https://app.quicktype.io/#l=cs&r=json2csharp
 * 
 * 
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.WebVowl
{
    public partial class Modes
    {
        [JsonProperty("checkBox")]
        public List<CheckBox> CheckBox { get; set; }

        [JsonProperty("colorSwitchState")]
        public bool ColorSwitchState { get; set; }
    }
}
