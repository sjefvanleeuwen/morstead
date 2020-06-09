using Microsoft.AspNetCore.Components;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components
{
    public partial class Header
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Link { get; set; } = "/";
        [Parameter]
        public string LinkScreenReaderDescription { get; set; }
    }
}
