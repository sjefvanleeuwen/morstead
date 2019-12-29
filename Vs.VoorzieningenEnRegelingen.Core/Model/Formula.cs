using System;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Formula : IIdentifiable
    {
        public Formula(DebugInfo debugInfo, string name, List<Function> functions)
        {
            DebugInfo = debugInfo ?? throw new ArgumentNullException(nameof(debugInfo));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Functions = functions ?? throw new ArgumentNullException(nameof(functions));
        }

        public Formula(DebugInfo debugInfo, string name, Function function)
        {
            DebugInfo = debugInfo ?? throw new ArgumentNullException(nameof(debugInfo));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (function == null) throw new ArgumentNullException(nameof(function));
            Functions = new List<Function>() { function };
        }

        public DebugInfo DebugInfo { get; }
        public string Name { get; }
        public List<Function> Functions { get; }
        /// <summary>
        /// A formula is situational if there's more than one function labeled by a situation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this formula is situational; otherwise, <c>false</c>.
        /// </value>
        public bool IsSituational => Functions.Count > 1;

    }
}
