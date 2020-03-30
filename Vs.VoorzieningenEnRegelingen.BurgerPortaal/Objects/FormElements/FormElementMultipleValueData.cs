using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class FormElementMultipleValueData : FormElementData, IFormElementMultipleValueDate
    {
        public IDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, FormElementLabel> Labels { get; set; } = new Dictionary<string, FormElementLabel>();

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

        public override void Validate(bool unobtrusive = false)
        {
            base.Validate(unobtrusive);

            var valid = ValidateValuesAreSet();

            if (!unobtrusive)
            {
                IsValid = valid;
            }
        }

        private bool ValidateValuesAreSet()
        {
            foreach (var item in Values)
            {
                if (string.IsNullOrWhiteSpace(item.Value))
                {
                    ErrorTexts.Add("Vul een waarde in voor alle waarden.");
                    return false;
                }
            }

            return true;
        }
    }
}
