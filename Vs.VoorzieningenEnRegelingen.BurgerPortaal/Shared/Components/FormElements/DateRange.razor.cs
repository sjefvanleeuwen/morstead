using System;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class DateRange
    {
        private IDateRangeFormElementData _data =>
            Data as IDateRangeFormElementData ??
                throw new ArgumentException($"The provided data element is not of type {nameof(IDateRangeFormElementData)}");

        public override bool HasInput => true;
    }
}
