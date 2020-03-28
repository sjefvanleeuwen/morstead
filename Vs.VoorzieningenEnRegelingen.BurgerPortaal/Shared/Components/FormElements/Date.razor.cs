using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Date
    {
        private IDateFormElementData _data => Data as IDateFormElementData;

        private string Year
        {
            get => _data.Values["year"];
            set => _data.Values["year"] = value;
        }

        private string Month
        {
            get => _data.Values["month"];
            set => _data.Values["month"] = value;
        }

        private string Day
        {
            get => _data.Values["day"];
            set => _data.Values["day"] = value;
        }
    }
}
