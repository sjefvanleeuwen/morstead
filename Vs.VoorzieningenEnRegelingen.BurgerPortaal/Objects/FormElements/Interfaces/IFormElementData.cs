using Microsoft.AspNetCore.Components;
using System.Globalization;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IFormElementData
    {
        string ErrorText { get; }
        string HintText { get; set; }
        bool IsDisabled { get; set; }
        bool IsRequired { get; set; }
        bool IsValid { get; set; }
        string Label { get; set; }
        string Name { get; set; }
        FormElementSize Size { get; set; }
        string TagText { get; set; }
        TypeInference.InferenceResult.TypeEnum InferedType { get; set; }
        CultureInfo Culture { get; set; }
        string Value { get; set; }
        EventCallback<string> ValueChanged { get; set; }
        string ElementSize { get; }

        void Validate(bool unobtrusive = false);
        void FillFromExecutionResult(IExecutionResult result, IContentController contentController);
    }
}