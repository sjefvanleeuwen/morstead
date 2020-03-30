using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Date
    {
        private IDateFormElementData _data => Data as IDateFormElementData;

        private string Year
        {
            get => _data.GetYear().ToString();
            set => _data.SetYear(value);
        }

        private string Month
        {
            get => _data.GetMonth().ToString();
            set => _data.SetMonth(value);
        }

        private string Day
        {
            get => _data.GetDay().ToString();
            set => _data.SetDay(value);
        }
    }
}
