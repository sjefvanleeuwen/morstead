using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Site.Model;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared.Components
{
    public partial class NavigationMenuItem
    {
        [Parameter]
        public MenuItem Content { get; set; }
    }
}
