using System;

namespace Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces
{
    public interface IDateRangeFormElementData : IFormElementData
    {
        DateTime MinimumAllowedDate { get; set; }
        DateTime MaximumAllowedDate { get; set; }
        DateTime? ValueDateStart { get; set; }
        DateTime? ValueDateEnd { get; set; }
        DateTime? ValueUtcDateStart { get => ValueDateStart?.ToUniversalTime(); set => ValueDateStart = value?.ToLocalTime(); }
        DateTime? ValueUtcDateEnd { get => ValueDateEnd?.ToUniversalTime(); set => ValueDateEnd = value?.ToLocalTime(); }
        string LabelStart { get; set; }
        string LabelEnd { get; set; }
    }
}