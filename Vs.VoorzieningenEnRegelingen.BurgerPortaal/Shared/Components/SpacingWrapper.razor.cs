using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components
{
    public partial class SpacingWrapper
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
