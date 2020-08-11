using System;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class Menu : IMenu
    {
        public IEnumerable<IMenuItem> MenuItems { get; private set; } = new List<IMenuItem>();

        public event Action OnChange;

        public void SetMenuItems(IEnumerable<IMenuItem> menuItems)
        {
            MenuItems = menuItems;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
