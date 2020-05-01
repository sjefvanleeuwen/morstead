using Microsoft.AspNetCore.Components;

namespace Vs.BurgerPortaal.Core.Shared.Components
{
    public partial class TimelineItem
    {
        [Parameter]
        public int? Number { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Content { get; set; }
    }

    public class TimelineItemDTO
    {
        public int? Number { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}