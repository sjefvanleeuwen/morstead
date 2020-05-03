using Microsoft.AspNetCore.Components;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components
{
    public partial class Badge
    {
        [Parameter]
        public int? Number { get; set; }

        private bool Show => (Number ?? 0) != 0;
    }
}
