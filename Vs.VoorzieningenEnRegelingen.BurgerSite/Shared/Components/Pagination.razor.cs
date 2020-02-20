using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerSite.Shared.Components
{
    public partial class Pagination
    {
        [Parameter]
        public string Next { get; set; }
        [Parameter]
        public string NextText { get; set; }
        [Parameter]
        public string Previous { get; set; }
        [Parameter]
        public string PreviousText { get; set; }
        [Parameter]
        public string ScreenreaderDescription { get; set; }
    }
}
