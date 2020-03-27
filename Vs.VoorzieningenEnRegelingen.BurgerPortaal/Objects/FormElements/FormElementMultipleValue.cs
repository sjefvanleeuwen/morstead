using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class FormElementMultipleValue : FormElementData, IFormElementMultipleValue
    {
        protected string values = string.Empty;
        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    }
}
