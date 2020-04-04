using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core.Interface
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