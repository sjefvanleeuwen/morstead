using System.Collections.Generic;
using System.Linq;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class NumericFormElementData : FormElementData, INumericFormElementData
    {
        public int? Decimals { get; set; } = null;

        public bool DecimalsOptional { get; set; }

        public override void CustomValidate(bool unobtrusive = false)
        {
            base.CustomValidate(unobtrusive);
            if (!IsValid)
            {
                return;
            }
            var chars = new List<char>(Value.ToCharArray());
            var validChars = new List<char>("1234567890,".ToCharArray());
            foreach (var c in Value)
            {
                if (!validChars.Contains(c))
                {
                    ErrorTexts.Add("Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.");
                    if (!unobtrusive)
                    {
                        IsValid = false;
                    }
                    break;
                }
            }
            if (chars.Where(c => c == ',').Count() > 1)
            {
                ErrorTexts.Add("Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.");
                if (!unobtrusive)
                {
                    IsValid = false;
                }
            }
            var parts = Value.Split(',');
            if (parts.Count() == 2 && parts[1].Length != 2)
            {
                ErrorTexts.Add("Typ twee cijfers achter de komma.");
                if (!unobtrusive)
                {
                    IsValid = false;
                }
            }
        }

        public override void FillFromExecutionResult(IExecutionResult result, IContentController contentController)
        {
            base.FillFromExecutionResult(result, contentController);

            Size = FormElementSize.Large;
            Decimals = 2;
            DecimalsOptional = true;
        }
    }
}
