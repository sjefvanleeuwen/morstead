using Vs.Core.Semantic;
using static Vs.VoorzieningenEnRegelingen.Core.TypeInference.InferenceResult;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
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