using Vs.Rules.Core.Model;

namespace Vs.Rules.Core.Interfaces
{
    public interface IContentNode
    {
        bool IsBreak { get; set; }
        bool IsSituational { get; set; }
        string Name { get; }
        IParameter Parameter { get; set; }
        string Situation { get; set; }
    }
}