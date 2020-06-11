using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Site.Model.Tables.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared.Components
{
    public partial class Table
    {
        [Parameter]
        public IGrid Grid { get; set; }

        private IEnumerable<string> _columnNames;
        public IEnumerable<string> ColumnNames
        {
            get
            {
                if (_columnNames == null || !_columnNames.Any())
                {
                    _columnNames = Grid.GetColumnNames();
                }
                return _columnNames;
            }
        }
    }
}
