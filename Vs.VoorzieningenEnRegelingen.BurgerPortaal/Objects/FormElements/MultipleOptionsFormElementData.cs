using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class MultipleOptionsFormElementData : FormElementData, IMultipleOptionsFormElementData
    {
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();
    }
}
