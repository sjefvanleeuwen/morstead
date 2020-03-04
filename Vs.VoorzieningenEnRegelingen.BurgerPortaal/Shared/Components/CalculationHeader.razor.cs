using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components
{
    public partial class CalculationHeader
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string SubTitle { get; set; }
        [Parameter]
        public int Number { get; set; }
        [Parameter]
        public string Subject { get; set; }
    }
}
