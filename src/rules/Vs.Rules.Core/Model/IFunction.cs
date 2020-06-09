using Vs.Core.Diagnostics;

namespace Vs.Rules.Core.Model
{
    public interface IFunction
    {
        DebugInfo DebugInfo { get; }
        string Expression { get; }
        bool IsSituational { get; }
        string SemanticKey { get; set; }
        string Situation { get; }
    }
}