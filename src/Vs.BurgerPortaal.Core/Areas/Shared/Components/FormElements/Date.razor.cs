using System;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements
{
    public partial class Date
    {
        private IDateFormElementData _data =>
            Data as IDateFormElementData ??
                throw new ArgumentException($"The provided data element is not of type {nameof(IDateFormElementData)}");

        public override bool HasInput => true;
    }
}
