using Microsoft.AspNetCore.Components;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IFormElementData
    {
        string ErrorText { get; set; }
        string HintText { get; set; }
        bool IsDisabled { get; set; }
        bool IsRequired { get; set; }
        bool IsValid { get; set; }
        string Label { get; set; }
        string Name { get; set; }
        FormElementSize Size { get; set; }
        string TagText { get; set; }
        string Value { get; set; }
        EventCallback<string> ValueChanged { get; set; }
        TypeInference.InferenceResult.TypeEnum InferedType { get; set; }
        string ElementSize { get; }
        void Validate(bool unobtrusive);
        void FillFromExecutionResult(IExecutionResult result);
    }
}