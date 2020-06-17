using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public IEnumerable<MenuItem> SubMenuItems { get; set; }
        public bool IsDivider { get; set; }
    }
}
