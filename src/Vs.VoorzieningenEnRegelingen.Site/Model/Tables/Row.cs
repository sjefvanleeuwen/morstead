using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Site.Model.Tables.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Tables
{
    public class Row : IRow
    {
        public IEnumerable<IDisplayItem> DisplayItems { get; set; }

        public IEnumerable<string> Keys => DisplayItems.Select(d => d.Name);

        public IEnumerable<string> VisibleKeys => DisplayItems.Where(d => d.Display).Select(d => d.Name);

        public IEnumerable<ITableAction> TableActions { get; set; }

        public string GetValueFor(string key) => DisplayItems?.FirstOrDefault(d => d.Name == key)?.Value ?? string.Empty;
    }
}
