using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerSite.Shared.Components
{
    public partial class ButtonSubmit
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
