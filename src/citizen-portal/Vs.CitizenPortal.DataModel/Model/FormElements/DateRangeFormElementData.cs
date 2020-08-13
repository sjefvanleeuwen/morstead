﻿using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Core.Enums;
using Vs.Rules.Core.Interfaces;

namespace Vs.CitizenPortal.DataModel.Model.FormElements
{
    public class DateRangeFormElementData : FormElementData, IDateRangeFormElementData
    {

        public DateTime MinimumAllowedDate { get; set; } = DateTime.MinValue;
        public DateTime MaximumAllowedDate { get; set; } = DateTime.MaxValue;
        public DateTime? ValueDateStart { get; set; }
        public DateTime? ValueDateEnd { get; set; }
        public DateTime? ValueUtcDateStart { get => ValueDateStart?.ToUniversalTime(); set => ValueDateStart = value?.ToLocalTime(); }
        public DateTime? ValueUtcDateEnd { get => ValueDateEnd?.ToUniversalTime(); set => ValueDateEnd = value?.ToLocalTime(); }
        public string LabelStart { get; set; }
        public string LabelEnd { get; set; }

        public override string Value
        {
            get => GetDateRangeAsString();
            set => SetDateRange(value);
        }

        public DateRangeFormElementData()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = Culture; //nl-NL
        }

        private string GetDateRangeAsString()
        {
            return
                ValueDateStart != null && ValueDateEnd != null ?
                    new TimeRange(ValueDateStart.Value, ValueDateEnd.Value).ToString() :
                    string.Empty;
        }

        private void SetDateRange(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                ValueDateStart = null;
                ValueDateEnd = null;
                return;
            }

            var dateTimeStrings = value.Split('|')[0].Split(" - ").ToList();
            //in case the startdate and enddate are the same
            if (dateTimeStrings.Count == 1)
            {
                dateTimeStrings.Add(dateTimeStrings[0]);
            }
            try
            {
                ValueDateStart = StringToDate(dateTimeStrings[0]);
                ValueDateEnd = StringToDate(dateTimeStrings[1]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Unable to process the timerange format  provided '{value}'.", ex);
            }
        }

        private DateTime StringToDate(string value)
        {
            base.value = value;
            if (DateTime.TryParse(value, Culture, DateTimeStyles.None, out DateTime date)
                || DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
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

            if (ValueDateStart > ValueDateEnd)
            {
                errors.Add($"De startdatum is groter dan de einddatum.");
            }
            if (ValueDateStart < MinimumAllowedDate)
            {
                errors.Add($"De startdatum is kleiner dan de minimaal toegestane datum: '{MinimumAllowedDate.ToString("d", Culture)}'.");
            }
            //ValueDateStart compared to maximum date is not needed to check; then the enddate would en smaller than the start date
            if (ValueDateEnd > MaximumAllowedDate)
            {
                errors.Add($"De einddatum is groter dan de maximaal toegestane datum: '{MaximumAllowedDate.ToString("d", Culture)}'.");
            }
            //ValueDateEnd compared to minimum date is not needed to check; then the enddate would en smaller than the start date

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

            var parameterSemanticKey = result.GetParameterSemanticKey();
            var labelStart = contentController.GetText(parameterSemanticKey, FormElementContentType.LabelStart);
            if (!string.IsNullOrWhiteSpace(labelStart))
            {
                LabelStart = labelStart;
            }
            var labelEnd = contentController.GetText(parameterSemanticKey, FormElementContentType.LabelEnd);
            if (!string.IsNullOrWhiteSpace(labelEnd))
            {
                LabelEnd = labelEnd;
            }
        }
    }
}
