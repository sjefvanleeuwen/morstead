using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class WrapperDefault
    {
        [Parameter]
        public bool IsValid { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
