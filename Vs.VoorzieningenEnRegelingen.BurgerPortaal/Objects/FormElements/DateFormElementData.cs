using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class DateFormElementData : FormElementData, IDateFormElementData
    {
        public DateTime MinimumAllowedDate { get; set; } = DateTime.MinValue;
        public DateTime MaximumAllowedDate { get; set; } = DateTime.MaxValue;
        public DateTime? ValueDate { get; set; }
        public DateTime? ValueUtcDate { get => ValueDate?.ToUniversalTime(); set => ValueDate = value?.ToLocalTime(); }

        public override string Value
        {
            get => GetDateAsString();
            set => SetDate(value);
        }

        private string GetDateAsString()
        {
            return ValueDate?.ToString("yyyy-MM-dd") ?? string.Empty;
        }

        private void SetDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                ValueDate = null;
                return;
            }

            //base.value = value;
            if (DateTime.TryParse(value, Culture, DateTimeStyles.None, out DateTime date)
                || DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                ValueDate = date;
            }
            else
            {
                throw new ArgumentException($"The date provided could not be parsed as universal or culture: '{Culture.Name}'.");
            }
        }

        public override void CustomValidate(bool unobtrusive = false)
        {
            base.CustomValidate();

            var errors = new List<string>();
            if (ValueDate < MinimumAllowedDate)
            {
                errors.Add($"De datum is kleiner dan de minimaal toegestane datum: '{MinimumAllowedDate.ToString("d", Culture)}'.");
            }
            if (ValueDate > MaximumAllowedDate)
            {
                errors.Add($"De datum is groter dan de maximaal toegestane datum: '{MaximumAllowedDate.ToString("d", Culture)}'.");
            }

            if (!errors.Any())
            {
                return;
            }

            ((List<string>)ErrorTexts).AddRange(errors);
            if (!unobtrusive)
            {
                IsValid = false;
            }
        }
    }
}
