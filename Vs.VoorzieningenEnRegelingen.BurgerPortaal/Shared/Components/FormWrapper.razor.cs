using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components
{
    public partial class FormWrapper
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
