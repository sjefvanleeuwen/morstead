using System;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IDateRangeFormElementData : IFormElementMultipleValueDate
    {
        DateTime MinimumAllowedDate { get; set; }
        DateTime MaximumAllowedDate { get; set; }
        Dictionary<DateRangeType, DateTime> ValueDates { get; set; }

        int GetYear(DateRangeType type);
        void SetYear(DateRangeType type, string value);
        int GetMonth(DateRangeType type);
        void SetMonth(DateRangeType type, string value);
        int GetDay(DateRangeType type);
        void SetDay(DateRangeType type, string value);
    }
}