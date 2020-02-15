using System;
using Vs.Core.Diagnostics;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Function
    {
        public Function(DebugInfo debugInfo, string situation, string expression)
        {
            DebugInfo = debugInfo ?? throw new ArgumentNullException(nameof(debugInfo));

            Situation = situation;
            Expression = expression;
        }

        public Function(DebugInfo debugInfo, string expression)
        {
            DebugInfo = debugInfo ?? throw new ArgumentNullException(nameof(debugInfo));
            Expression = expression;
        }

        public bool IsSituational => Situation != null;

        public DebugInfo DebugInfo { get; }
        public string Situation { get; }
        public string Expression { get; }
    }
}
