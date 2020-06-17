using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Site.Model;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared.Components
{
    public partial class NavigationMenu
    {
        [Parameter]
        public IEnumerable<MenuItem> Menu { get; set; } = new List<MenuItem>();

        public void BuildMenu(IEnumerable<MenuItem> menu)
        {
            Menu = menu;
            this.StateHasChanged();
        }
    }
}
