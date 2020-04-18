using System;
using Vs.Core.Diagnostics;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Column
    {
        public Column(DebugInfo debugInfo, object value)
        {
            DebugInfo = debugInfo ?? throw new ArgumentNullException(nameof(debugInfo));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DebugInfo DebugInfo { get; }
        public object Value { get; }
    }
}
