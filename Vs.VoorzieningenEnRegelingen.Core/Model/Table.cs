using System;
using System.Collections.Generic;
using System.Linq;
using Vs.Core.Diagnostics;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Table
    {
        public Table(DebugInfo debugInfo, string name, List<ColumnType> columnTypes, List<Row> rows, IEnumerable<ISituation> situations = null)
        {
            DebugInfo = debugInfo ?? throw new ArgumentNullException(nameof(debugInfo));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ColumnTypes = columnTypes ?? throw new ArgumentNullException(nameof(columnTypes));
            Rows = rows ?? throw new ArgumentNullException(nameof(rows));
            Situations = situations;
            if (Rows.Count == 0) throw new ArgumentNullException(nameof(rows));
            for (int i = 0; i < ColumnTypes.Count; i++)
            {
                 ColumnTypes[i].Index = i;
                // evaluate one row for type inference
                ColumnTypes[i].Type = TypeInference.Infer(Rows[0].Columns[i].Value.ToString()).Type;
            }
        }

        public DebugInfo DebugInfo { get; }
        public string Name { get; }
        public List<ColumnType> ColumnTypes { get; }
        public List<Row> Rows { get; }
        public IEnumerable<ISituation> Situations { get; }
        public bool IsSituational { get { return (Situations != null && Situations.Any()); } }
    }
}
