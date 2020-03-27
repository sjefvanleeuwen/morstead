using System;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IDateFormElementData : IFormElementSingleValue
    {
        DateTime MinimumAllowedDate { get; set; }
        DateTime MaximumAllowedDate { get; set; }
        DateTime ValueDate { get; set; }
    }
}