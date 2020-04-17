using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IFormElementData : IValidatableObject
    {
        string ErrorText { get; }
        string HintText { get; set; }
        bool IsDisabled { get; set; }
        bool IsRequired { get; set; }
        bool IsValid { get; set; }
        string Label { get; set; }
        string Name { get; set; }
        TypeInference.InferenceResult.TypeEnum InferedType { get; set; }
        CultureInfo Culture { get; set; }
        string Value { get; set; }
        EventCallback<string> ValueChanged { get; set; }

        void CustomValidate(bool unobtrusive = false);
        void FillFromExecutionResult(IExecutionResult result, IContentController contentController);
    }
}