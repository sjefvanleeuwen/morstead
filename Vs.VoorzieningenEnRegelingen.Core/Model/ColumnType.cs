using System;
using Vs.Core.Diagnostics;
using static Vs.VoorzieningenEnRegelingen.Core.TypeInference.InferenceResult;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class ColumnType
    {
        public ColumnType(DebugInfo debugInfo, string name)
        {
            DebugInfo = debugInfo ?? throw new ArgumentNullException(nameof(debugInfo));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Index { get; internal set; }
        public DebugInfo DebugInfo { get; }
        public string Name { get; }
        public TypeEnum Type { get; internal set; }
    }
}
