using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerSite.Shared.Components
{
    public partial class Badge
    {
        [Parameter]
        public int Number { get; set; } = 0;
    }
}
