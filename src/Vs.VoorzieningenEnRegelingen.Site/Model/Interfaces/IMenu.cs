using System;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IMenu
    {
        IEnumerable<IMenuItem> MenuItems { get; }

        event Action OnChange;

        void SetMenuItems(IEnumerable<IMenuItem> menuItems);
    }
}