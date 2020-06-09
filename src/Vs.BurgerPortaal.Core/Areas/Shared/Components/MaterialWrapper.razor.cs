using Microsoft.AspNetCore.Components;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components
{
    public partial class MaterialWrapper
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
