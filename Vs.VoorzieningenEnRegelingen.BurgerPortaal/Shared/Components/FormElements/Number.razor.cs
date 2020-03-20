using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Number
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            AddDecimalsToValue();
        }

        private void AddDecimalsToValue()
        {
            //if value is set and decimals is set
            if (!string.IsNullOrWhiteSpace(Data.Value) && (Data.Decimals ?? -1) <= 0)
            {
                return;
            }

            //prepare the value for parsing
            var value = Data.Value?.Replace(',', '.');
            //continue only if parsable
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double d))
            {
                return;
            }

            //continue only if decimals are optional and there are decimals
            if (Data.DecimalsOptional && Math.Round(d) == d)
            {
                return;
            }

            Data.Value = d.ToString("#.00").Replace('.', ',');
        }
    }
}
