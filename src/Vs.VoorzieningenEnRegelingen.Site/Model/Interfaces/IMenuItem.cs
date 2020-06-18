using System.Collections;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IMenuItem
    {
        bool IsDivider { get; set; }
        string Link { get; set; }
        string Name { get; set; }
        IDictionary<string, object> HtmlAttributes { get; set; }
        System.Collections.Generic.IEnumerable<IMenuItem> SubMenuItems { get; set; }
    }
}