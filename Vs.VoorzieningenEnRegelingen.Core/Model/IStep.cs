using System.Collections.Generic;
using Vs.Core.Semantic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public interface IStep : ISemanticKey
    {
        IBreak Break { get; }
        string Description { get; }
        string Formula { get; }
        bool IsSituational { get; }
        int Key { get; }
        string Name { get; }
        string Situation { get; }
        string Value { get; }
        IEnumerable<string> Choices { get; }
    }
}