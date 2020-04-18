using Vs.Core.Semantic;

namespace Vs.Rules.Core.Model
{
    public interface IBreak : ISemanticKey
    {
        public string Expression { get; set; }
    }
}