using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class DateRangeFormElementData : FormElementMultipleValueData, IDateRangeFormElementData
    {
        public DateTime MinimumAllowedDate { get; set; } = DateTime.MinValue;
        public DateTime MaximumAllowedDate { get; set; } = DateTime.MaxValue;
        public Dictionary<DateRangeType, DateTime> ValueDates { get; set; }

        public int GetYear(DateRangeType type) => ValueDates[type].Year;

        public void SetYear(DateRangeType type, string value)
        {
            //set the actually filled value
            Values[type + "year"] = value;
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
            ValueDates[type] = new DateTime(year, GetMonth(type), GetDay(type));
        }

        public int GetMonth(DateRangeType type) => ValueDates[type].Month;

        public void SetMonth(DateRangeType type, string value)
        {
            Values[type + "month"] = value;
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
            ValueDates[type] = new DateTime(GetYear(type), month, GetDay(type));
        }

        public int GetDay(DateRangeType type) => ValueDates[type].Day;

        public void SetDay(DateRangeType type, string value)
        {
            Values[type + "day"] = value;
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
                proposedDate = new DateTime(GetYear(type), GetMonth(type), day);
            }
            catch (ArgumentOutOfRangeException)
            {
                //an invalid day for the month has been entered
                return;
            }

            ValueDates[type] = proposedDate;
        }

        public override string Value
        {
            get => GetDateRange();
            set => SetDateRange(value);
        }

        public DateRangeFormElementData()
        {
            ValueDates = new Dictionary<DateRangeType, DateTime>
            {
                { DateRangeType.Start, DateTime.MinValue },
                { DateRangeType.End, DateTime.MaxValue }
            };

            System.Threading.Thread.CurrentThread.CurrentCulture = Culture; //nl-NL

            SetDateRange(new TimeRange(DateTime.Today, DateTime.Today).ToString());
        }

        private string GetDateRange()
        {
            if (!FieldsContainValidDate())
            {
                string startyear = "unknown", startmonth = "unknown", startday = "unknown";
                if (!Values.ContainsKey(DateRangeType.Start + "year"))
                {
                    throw new KeyNotFoundException("The value for startyear has not been set.");
                }
                if (!Values.ContainsKey(DateRangeType.Start + "month"))
                {
                    throw new KeyNotFoundException("The value for startmonth has not been set.");
                }
                if (!Values.ContainsKey(DateRangeType.Start + "day"))
                {
                    throw new KeyNotFoundException("The value for startday has not been set.");
                }
                string endyear = "unknown", endmonth = "unknown", endday = "unknown";
                if (!Values.ContainsKey(DateRangeType.End + "year"))
                {
                    throw new KeyNotFoundException("The value for endyear has not been set.");
                }
                if (!Values.ContainsKey(DateRangeType.End + "month"))
                {
                    throw new KeyNotFoundException("The value for endmonth has not been set.");
                }
                if (!Values.ContainsKey(DateRangeType.End + "day"))
                {
                    throw new KeyNotFoundException("The value for endday has not been set.");
                }
                throw new FormatException($"The values defined do not form two valid dates '{startyear}-{startmonth}-{startday}' and '{endyear}-{endmonth}-{endday}'");
            }

            return new TimeRange(ValueDates[DateRangeType.Start], ValueDates[DateRangeType.End]).ToString();
        }

        private void SetDateRange(string value)
        {
            var dateTimeStrings = value.Split('|')[0].Split(" - ").ToList();
            //in case the startdate and enddate are the same
            if (dateTimeStrings.Count == 1)
            {
                dateTimeStrings.Add(dateTimeStrings[0]);
            }
            try
            {
                SetDate(DateRangeType.Start, dateTimeStrings[0]);
                SetDate(DateRangeType.End, dateTimeStrings[1]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Unable to process the timerange format  provided '{value}'.", ex);
            }
        }
        private void SetDate(DateRangeType type, string value)
        {
            base.value = value;
            if (DateTime.TryParse(value, Culture, DateTimeStyles.None, out DateTime date)
                || DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                SetYear(type, date.Year.ToString());
                SetMonth(type, date.Month.ToString());
                SetDay(type, date.Day.ToString());
                ValueDates[type] = date;
            }
            else
            {
                throw new ArgumentException($"The date provided could not be parsed as universal or culture: '{Culture.Name}'.");
            }
        }

        public override void Validate(bool unobtrusive = false)
        {
            base.Validate();

            var errors = new List<string>();

            if (!FieldsContainValidDate())
            {
                errors.Add("De waarden ingegeven vormen samen geen geldige datum range.");
            }
            if (ValueDates[DateRangeType.Start] > ValueDates[DateRangeType.End])
            {
                errors.Add($"De startdatum is groter dan de einddatum.");
            }
            if (ValueDates[DateRangeType.Start] < MinimumAllowedDate)
            {
                errors.Add($"De startdatum is kleiner dan de minimaal toegestane datum: '{MinimumAllowedDate.ToString("d", Culture)}'.");
            }
            if (ValueDates[DateRangeType.End] > MaximumAllowedDate)
            {
                errors.Add($"De einddatum is groter dan de maximaal toegestane datum: '{MaximumAllowedDate.ToString("d", Culture)}'.");
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
                if (int.Parse(Values[DateRangeType.Start + "year"]) == ValueDates[DateRangeType.Start].Year &&
                    int.Parse(Values[DateRangeType.Start + "month"]) == ValueDates[DateRangeType.Start].Month &&
                    int.Parse(Values[DateRangeType.Start + "day"]) == ValueDates[DateRangeType.Start].Day &&
                    int.Parse(Values[DateRangeType.End + "year"]) == ValueDates[DateRangeType.End].Year &&
                    int.Parse(Values[DateRangeType.End + "month"]) == ValueDates[DateRangeType.End].Month &&
                    int.Parse(Values[DateRangeType.End + "day"]) == ValueDates[DateRangeType.End].Day)
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
