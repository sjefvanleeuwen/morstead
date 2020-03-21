using System.Collections.Generic;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class NumericFormElementData : FormElementData, INumericFormElementData
    {
        public int? Decimals { get; set; } = null;

        public bool DecimalsOptional { get; set; }

        public override bool ValidateForType(out string errorText)
        {
            errorText = string.Empty;
            var chars = new List<char>(Value.ToCharArray());
            var validChars = new List<char>("1234567890,".ToCharArray());
            foreach (var c in Value)
            {
                if (!validChars.Contains(c))
                {
                    errorText = "Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.";
                    return false;
                }
            }
            if (chars.Where(c => c == ',').Count() > 1)
            {
                errorText = "Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.";
                return false;
            }
            var parts = Value.Split(',');
            if (parts.Count() == 2 && parts[1].Length != 2)
            {
                errorText = "Typ twee cijfers achter de komma.";
                return false;
            }

            return true;
        }
    }
}
