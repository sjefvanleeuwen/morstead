using System;

namespace Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces
{
    public interface IDateFormElementData : IFormElementData
    {
        DateTime MinimumAllowedDate { get; set; }
        DateTime MaximumAllowedDate { get; set; }
        DateTime? ValueDate { get; set; }
        DateTime? ValueUtcDate { get; set; }
    }
}