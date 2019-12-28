using System;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Row
    {
        public Row(DebugInfo debugInfo, List<Column> columns)
        {
            DebugInfo = debugInfo ?? throw new ArgumentNullException(nameof(debugInfo));
            Columns = columns ?? throw new ArgumentNullException(nameof(columns));
        }

        public DebugInfo DebugInfo { get; }
        public List<Column> Columns { get; }
    }
}
