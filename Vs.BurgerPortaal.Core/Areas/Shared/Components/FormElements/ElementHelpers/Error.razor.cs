using Microsoft.AspNetCore.Components;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements.ElementHelpers
{
    public partial class Error
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private bool Show => ChildContent != null && !string.IsNullOrWhiteSpace(ChildContent.ToString());
    }
}
