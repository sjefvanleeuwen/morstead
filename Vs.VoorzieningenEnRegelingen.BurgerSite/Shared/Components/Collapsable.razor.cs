using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerSite.Shared.Components
{
    public partial class Collapsable
    {
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string CollapsedText { get; set; } = "ingeklapt";
        [Parameter]
        public string UnfoldedText { get; set; } = "uitgeklapt";
        [Parameter]
        public RenderFragment Title { get; set; }
        [Parameter]
        public RenderFragment CollapsableContent { get; set; }
    }
}
