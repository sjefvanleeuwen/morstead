using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Site.Model.Tables.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Tables
{
    public class Grid : IGrid
    {
        public IEnumerable<IRow> Rows { get; set; }

        public IEnumerable<string> GetColumnNames()
        {
            if (!Rows.Any())
            {
                return new List<string>();
            }
            else
            {
                return Rows.ElementAt(0).VisibleKeys;
            }
        }
    }
}
