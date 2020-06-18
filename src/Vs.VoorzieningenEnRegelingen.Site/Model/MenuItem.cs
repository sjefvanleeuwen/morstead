using System;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class MenuItem : IMenuItem
    {
        public bool IsDivider { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public IDictionary<string, object> HtmlAttributes { get; set; }
        public Action OnClick { get; set; }
        public IEnumerable<IMenuItem> SubMenuItems { get; set; }
    }
}
