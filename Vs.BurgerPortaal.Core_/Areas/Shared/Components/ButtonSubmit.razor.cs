using Microsoft.AspNetCore.Components;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components
{
    public partial class ButtonSubmit
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
