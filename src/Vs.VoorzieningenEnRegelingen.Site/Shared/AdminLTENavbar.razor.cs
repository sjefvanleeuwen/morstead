using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.VoorzieningenEnRegelingen.Site.Shared.Components;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared
{
    public partial class AdminLTENavbar
    {
        private NavigationMenu _navigationMenu { get; set; }

        public void BuildMenu(IEnumerable<MenuItem> Menu)
        {
            _navigationMenu.BuildMenu(Menu);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                var menu = new List<MenuItem>
                {
                    new MenuItem { Link = "/", Name = "Index" },
                    new MenuItem { Link = "/About", Name = "Over"},
                    new MenuItem { Name = "Sub", SubMenuItems =
                        new List<MenuItem> {
                            new MenuItem { Link = "/", Name = "Index" },
                            new MenuItem { IsDivider = true },
                            new MenuItem { Link = "/About", Name = "Over"}
                        }
                    }
                };
                BuildMenu(menu);
            }
            base.OnAfterRender(firstRender);
        }
    }
}
