using System;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IDateFormElementData : IFormElementMultipleValueDate
    {
        DateTime MinimumAllowedDate { get; set; }
        DateTime MaximumAllowedDate { get; set; }
        DateTime ValueDate { get; set; }

        int GetYear();
        void SetYear(string value);
        int GetMonth();
        void SetMonth(string value);
        int GetDay();
        void SetDay(string value);
    }
}