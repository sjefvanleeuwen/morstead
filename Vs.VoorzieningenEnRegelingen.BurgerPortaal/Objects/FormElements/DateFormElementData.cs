using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class DateFormElementData : FormElementMultipleValueData, IDateFormElementData
    {
        public DateTime MinimumAllowedDate { get; set; } = DateTime.MinValue;
        public DateTime MaximumAllowedDate { get; set; } = DateTime.MaxValue;
        public DateTime ValueDate { get; set; }

        public int GetYear() => ValueDate.Year;

        public void SetYear(string value)
        {
            //set the actually filled value
            Values["year"] = value;
            if (!int.TryParse(value, out int year))
            {
                //do nothing, value invalid.
                return;
            }
            if (year < 1 || year > 9999)
            {
                //do nothing, value invalid
                return;
            }
            ValueDate = new DateTime(year, GetMonth(), GetDay());
        }

        public int GetMonth() => ValueDate.Month;

        public void SetMonth(string value)
        {
            Values["month"] = value;
            if (!int.TryParse(value, out int month))
            {
                //do nothing, value invalid
                return;
            }
            if (month < 1 || month > 12)
            {
                //do nothing, value invalid
                return;
            }
            ValueDate = new DateTime(GetYear(), month, GetDay());
        }

        public int GetDay() => ValueDate.Day;

        public void SetDay(string value)
        {
            Values["day"] = value;
            if (!int.TryParse(value, out int day))
            {
                //do nothing, value invalid
                return;
            }
            if (day < 1 || day > 31)
            {
                //do nothing, value invalid
                return;
            }
            DateTime proposedDate;
            try
            {
                proposedDate = new DateTime(GetYear(), GetMonth(), day);
            }
            catch (ArgumentOutOfRangeException)
            {
                //an invalid day for the month has been entered
                return;
            }

            ValueDate = proposedDate;
        }

        public override string Value
        {
            get => GetDate();
            set => SetDate(value);
        }

        public DateFormElementData()
        {
            SetDate(DateTime.Today.ToString("yyyy-MM-dd"));
        }

        private string GetDate()
        {
            if (!FieldsContainValidDate())
            {
                string year = "unknown", month = "unknown", day = "unknown";
                if (!Values.ContainsKey("year"))
                {
                    throw new KeyNotFoundException("The value for year has not been set.");
                }
                if (!Values.ContainsKey("month"))
                {
                    throw new KeyNotFoundException("The value for month has not been set.");
                }
                if (!Values.ContainsKey("day"))
                {
                    throw new KeyNotFoundException("The value for day has not been set.");
                }
                throw new FormatException($"The values defined do not form a valid date '{year}-{month}-{day}'");
            }

            return ValueDate.ToString("yyyy-MM-dd");
        }

        private void SetDate(string value)
        {
            base.value = value;
            int year, month, day;
            DateTime date;
            try
            {
                year = int.Parse(value.Substring(0, 4));
                month = int.Parse(value.Substring(5, 2));
                day = int.Parse(value.Substring(8, 2));
                SetYear(year.ToString());
                SetMonth(month.ToString());
                SetDay(day.ToString());
                ValueDate = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                if (DateTime.TryParse(value, Culture, DateTimeStyles.None, out date))
                {
                    SetYear(date.Year.ToString());
                    SetMonth(date.Month.ToString());
                    SetDay(date.Day.ToString());
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
            base.Validate();

            var errors = new List<string>();

            if (!FieldsContainValidDate())
            {
                errors.Add("De waarden ingegeven vormen samen geen geldige datum.");
            }
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

        private bool FieldsContainValidDate()
        {
            try
            {
                if (int.Parse(Values["year"]) == ValueDate.Year &&
                    int.Parse(Values["month"]) == ValueDate.Month &&
                    int.Parse(Values["day"]) == ValueDate.Day)
                {
                    return true;
                }
            }
            catch (FormatException)
            {
                //parsing went wrong
                return false;
            }

            return false;
        }
    }
}
