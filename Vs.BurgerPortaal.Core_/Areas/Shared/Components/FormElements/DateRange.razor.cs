using System;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements
{
    public partial class DateRange
    {
        private IDateRangeFormElementData _data =>
            Data as IDateRangeFormElementData ??
                throw new ArgumentException($"The provided data element is not of type {nameof(IDateRangeFormElementData)}");

        public override bool HasInput => true;
    }
}
