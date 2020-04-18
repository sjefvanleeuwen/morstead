using Vs.Core.Semantic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public interface IBreak : ISemanticKey
    {
        public string Expression { get; set; }
    }
}