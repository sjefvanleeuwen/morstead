using Microsoft.AspNetCore.Components;

namespace Vs.BurgerPortaal.Core.Shared.Components
{
    public partial class MaterialWrapper
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
