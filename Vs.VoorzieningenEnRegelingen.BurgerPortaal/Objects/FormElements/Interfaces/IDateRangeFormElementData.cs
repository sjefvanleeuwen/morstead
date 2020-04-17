using System;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IDateRangeFormElementData : IFormElementData
    {
        DateTime MinimumAllowedDate { get; set; }
        DateTime MaximumAllowedDate { get; set; }
        DateTime? ValueDateStart { get; set; }
        DateTime? ValueDateEnd { get; set; }
        string LabelStart { get; set; }
        string LabelEnd { get; set; }
    }
}