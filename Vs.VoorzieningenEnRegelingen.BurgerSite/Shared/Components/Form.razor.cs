using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerSite.Shared.Components
{
    public partial class Form
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
