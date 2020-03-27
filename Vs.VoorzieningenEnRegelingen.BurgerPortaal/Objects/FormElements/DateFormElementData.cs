using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class DateFormElementData : FormElementMultipleValue, IDateFormElementData
    {
        public DateTime MinimumAllowedDate { get; set; } = DateTime.MinValue;
        public DateTime MaximumAllowedDate { get; set; } = DateTime.MaxValue;
        public DateTime ValueDate { get; set; } = DateTime.Today;

        public override string Value
        {
            get => GetDateString();
            set => SetDateString(value);
        }

        private string GetDateString()
        {
            if (!Values.ContainsKey("year") || !Values.ContainsKey("month") || !Values.ContainsKey("day"))
            {
                throw new ArgumentNullException("Three values are needed to make up a date.");
            }
            int year, month, day;
            DateTime date;
            try
            {
                year = int.Parse(Values["year"]);
                month = int.Parse(Values["month"]);
                day = int.Parse(Values["day"]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"At least one of the arguments provided was not numeric; year: '{Values["year"]}', month: '{Values["month"]}', day: '{Values["day"]}'.", ex);
            }
            try
            {
                date = new DateTime(year, month, day);
                //make sure month 13 is not parsed to a date a day later
                if (date.Year.ToString() != Values["year"] ||
                    date.Month.ToString() != Values["month"] ||
                    date.Day.ToString() != Values["day"])
                {
                    throw new Exception("values out or range.");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"A valid date could not be formed from the supplied values; year: '{Values["year"]}', month: '{Values["month"]}', day: '{Values["day"]}'.", ex);
            }

            ValueDate = date;

            return date.ToString("yyyy-MM-dd");
        }

        private void SetDateString(string value)
        {
            base.value = value;
            int year, month, day;
            DateTime date;
            try
            {
                year = int.Parse(value.Substring(0, 4));
                month = int.Parse(value.Substring(5, 2));
                day = int.Parse(value.Substring(8, 2));
                Values["year"] = year.ToString();
                Values["month"] = month.ToString();
                Values["day"] = day.ToString();
                ValueDate = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                if (DateTime.TryParse(value, Culture, DateTimeStyles.None, out date))
                {
                    Values["year"] = date.Year.ToString();
                    Values["month"] = date.Month.ToString();
                    Values["day"] = date.Day.ToString();
                    ValueDate = date;
                }
                else
                {
                    throw new ArgumentException($"The date provided coult not be parsed as universal or culture: '{Culture.Name}'.");
                }
            }
        }

        public override void Validate(bool unobtrusive = false)
        {
            base.Validate(unobtrusive);
            if (!IsValid)
            {
                return;
            }

            var errors = new List<string>();
            try
            {
                GetDateString();
            }
            catch (Exception)
            {
                errors.Add("De waarden ingegeven vormen samen geen geldige datum.");
            }
            if (ValueDate < MinimumAllowedDate)
            {
                errors.Add($"De datum is kleiner dan de minimaal toegestane datum ({MinimumAllowedDate.ToString("d", Culture)}).");
            }
            if (ValueDate > MaximumAllowedDate)
            {
                errors.Add($"De datum is groter dan de maximaal toegestane datum ({MaximumAllowedDate.ToString("d", Culture)}).");
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

        public override void FillFromExecutionResult(IExecutionResult result, IContentController contentController)
        {
            base.FillFromExecutionResult(result, contentController);

            Size = FormElementSize.Large;
        }
    }
}
