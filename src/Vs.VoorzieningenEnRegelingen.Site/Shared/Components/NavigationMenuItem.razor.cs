using Microsoft.AspNetCore.Components;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared.Components
{
    public partial class NavigationMenuItem
    {
        [Parameter]
        public IMenuItem Content { get; set; }
    }
}
