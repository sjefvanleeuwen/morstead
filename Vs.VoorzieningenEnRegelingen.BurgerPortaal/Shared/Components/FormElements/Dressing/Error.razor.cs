using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Dressing
{
    public partial class Error
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private bool _show => ChildContent != null;
    }
}
