using System.Collections.Generic;
using Vs.Core;
using Vs.Core.Diagnostics;
using Vs.Core.Semantic;

namespace Vs.Rules.Core.Model
{
    public interface IStep : ISemanticKey, ICloneable<IStep>
    {
        DebugInfo DebugInfo { get; }
        IBreak Break { get; }
        string Description { get; }
        string Formula { get; }
        bool IsSituational { get; }
        int Key { get; }
        string Name { get; }
        string Situation { get; set; }
        string Value { get; }
        IEnumerable<IChoice> Choices { get; }
    }
}