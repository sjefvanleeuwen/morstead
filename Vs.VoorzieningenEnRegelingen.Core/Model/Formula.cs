using System;
using System.Collections.Generic;
using static Vs.VoorzieningenEnRegelingen.Core.TypeInference;

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

        public bool IsSituational => Functions.Count > 1;

        public InferenceResult Evaluate(IExpressionResolver resolver)
        {
            //CalculationEngine engine = new CalculationEngine();
            //var builder = engine.Formula(_expression).
            return null;
        }
    }
}
