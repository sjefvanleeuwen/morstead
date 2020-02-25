using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components
{
    public partial class ButtonSubmit
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
