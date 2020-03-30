using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class FormElementSingleValueData : FormElementData, IFormElementSingleValueDate
    {
        public override void Validate(bool unobtrusive = false)
        {
            base.Validate(unobtrusive);

            var valid = ValidateValueIsSet();

            if (!unobtrusive)
            {
                IsValid = valid;
            }
        }

        private bool ValidateValueIsSet()
        {
            var valid = !string.IsNullOrWhiteSpace(Value);
            if (!valid)
            {
                ErrorTexts.Add("Vul een waarde in.");
            }

            return valid;
        }
    }
}
