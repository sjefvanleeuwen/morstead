using Microsoft.AspNetCore.Components;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared.Components
{
    public partial class NavigationMenu
    {
        [Inject]
        public IMenu Menu { get; set; }

        protected override void OnInitialized()
        {
            Menu.OnChange += StateHasChanged;
            base.OnInitialized();
        }

        public void Dispose()
        {
            Menu.OnChange -= StateHasChanged;
        }
    }
}
