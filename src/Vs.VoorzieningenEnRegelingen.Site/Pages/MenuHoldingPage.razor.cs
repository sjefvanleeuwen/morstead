using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Pages
{
    public class MenuHoldingPage : ComponentBase
    {
        [Inject]
        public IMenu Menu { get; set; }

        protected override void OnInitialized()
        {
            SetMenu();
            base.OnInitialized();
        }

        protected virtual void SetMenu()
        {
            var menuItems = new List<IMenuItem>
                {
                    new MenuItem { Link = "/", Name = "Home" },
                    new MenuItem { Link = "/About", Name = "Over"},
                };
            Menu.SetMenuItems(menuItems);
        }
    }
}
