using Blazor.NLDesignSystem;
using Vs.CitizenPortal.DataModel.Enums;
using Vs.CitizenPortal.DataModel.Model;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements
{
    public class NldsFormElementBase : FormElementBase
    {
        public InputSize InputSize => ElementSizeToInputSize(Data.Size);

        private InputSize ElementSizeToInputSize(ElementSize size)
        {
            switch (size)
            {
                case ElementSize.ExtraSmall:
                    return InputSize.ExtraSmall;
                case ElementSize.Small:
                    return InputSize.Small;
                case ElementSize.Large:
                    return InputSize.Large;
                case ElementSize.ExtraLarge:
                    return InputSize.ExtraLarge;
                case ElementSize.Undefined:
                case ElementSize.Medium:
                default:
                    return InputSize.Medium;
            }
        }
    }
}
