using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Tables.Interfaces
{
    public interface IGrid
    {
        IEnumerable<IRow> Rows { get; set; }

        IEnumerable<string> GetColumnNames();
    }
}