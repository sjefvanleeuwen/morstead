using System;
using System.Collections.Generic;
using Vs.Core.Diagnostics;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Table
    {
        public Table(DebugInfo debugInfo, string name, List<ColumnType> columnTypes, List<Row> rows)
        {
            DebugInfo = debugInfo ?? throw new ArgumentNullException(nameof(debugInfo));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ColumnTypes = columnTypes ?? throw new ArgumentNullException(nameof(columnTypes));
            Rows = rows ?? throw new ArgumentNullException(nameof(rows));

            for (int i = 0; i < ColumnTypes.Count; i++)
            {
                ColumnTypes[i].Index = i;
            }
        }

        public DebugInfo DebugInfo { get; }
        public string Name { get; }
        public List<ColumnType> ColumnTypes { get; }
        public List<Row> Rows { get; }
    }
}
