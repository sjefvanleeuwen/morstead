using Microsoft.AspNetCore.Components;
using System;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Number
    {
        [Parameter]
        public int? Decimals { get; set; } = null;
        [Parameter]
        public bool DecimalsOptional { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            AddDecimalsToValue();
        }

        private void AddDecimalsToValue()
        {
            //if value is set and decimals is set
            if (!string.IsNullOrWhiteSpace(Value) && (Decimals ?? -1) <= 0)
            {
                return;
            }

            //prepare the value for parsing
            var value = Value.Replace(',', '.');
            //continue only if parsable
            if (!double.TryParse(value, out double d))
            {
                return;
            }

            //continue only if decimals are optional and there are decimals
            if (DecimalsOptional && Math.Round(d) == d)
            {
                return;
            }

            Value = d.ToString("#.00").Replace('.', ',');
        }
    }
}
