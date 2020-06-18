using System;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IMenuItem
    {
        bool IsDivider { get; set; }
        string Link { get; set; }
        string Name { get; set; }
        IDictionary<string, object> HtmlAttributes { get; set; }
        Action OnClick { get; set; }
        IEnumerable<IMenuItem> SubMenuItems { get; set; }
    }
}