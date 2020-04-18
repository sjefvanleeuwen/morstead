using Vs.Core.Semantic;
using static Vs.Rules.Core.TypeInference.InferenceResult;

namespace Vs.Rules.Core.Model
{
    public interface IParameter : ISemanticKey
    {
        string Name { get; set; }
        object Value { get; set; }
        TypeEnum Type { get; set; }
        string ValueAsString { get; set; }
        string TypeAsString { get; }
    }
}