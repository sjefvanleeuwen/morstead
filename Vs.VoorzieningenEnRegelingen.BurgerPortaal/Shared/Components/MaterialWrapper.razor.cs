using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components
{
    public partial class MaterialWrapper
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
