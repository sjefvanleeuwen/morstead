using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class FormElementMultipleValueData : FormElementData, IFormElementMultipleValueDate
    {
        protected string values = string.Empty;
        public IDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, FormElementLabel> Labels { get; set; } = new Dictionary<string, FormElementLabel>();

        public string GetDisplayValue(string key)
        {
            if (!Values.ContainsKey(key))
            {
                return string.Empty;
            }
            return Values[key];
        }

        public string GetLabelText(string key)
        {
            if (!Labels.ContainsKey(key))
            {
                return string.Empty;
            }
            return Labels[key].Text;
        }

        public string GetLabelTitle(string key)
        {
            if (!Labels.ContainsKey(key))
            {
                return string.Empty;
            }
            return Labels[key].Title;
        }
    }
}
