using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
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
        Dictionary<string, string> Options { get; set; }
        FormElementSize Size { get; set; }
        string TagText { get; set; }
        string Value { get; set; }
        EventCallback<string> ValueChanged { get; set; }
        TypeInference.InferenceResult.TypeEnum InferedType { get; set; }
        string ElementSize { get; }
        bool Validate(bool unobtrusive);
    }
}