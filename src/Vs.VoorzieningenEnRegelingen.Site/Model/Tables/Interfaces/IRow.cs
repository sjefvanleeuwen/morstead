using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Tables.Interfaces
{
    public interface IRow
    {
        IEnumerable<IDisplayItem> DisplayItems { get; set; }

        IEnumerable<string> Keys { get; }

        IEnumerable<string> VisibleKeys { get; }

        IEnumerable<ITableAction> TableActions { get; set; }

        string GetValueFor(string key);
    }
}