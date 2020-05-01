using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Vs.BurgerPortaal.Core.Shared.Components
{
    public partial class Timeline
    {
        [Parameter]
        public bool IndicateStart { get; set; } = false;
        [Parameter]
        public bool IndicateEnd { get; set; } = false;
        [Parameter]
        public bool IndicateEnds { get; set; } = false;
        [Parameter]
        public IEnumerable<TimelineItemDTO> Items { get; set; }

        private bool _indicateStart => IndicateEnds || IndicateStart;
        private bool _indicateEnd => IndicateEnds || IndicateEnd;
    }
}
