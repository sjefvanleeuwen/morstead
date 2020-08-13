using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Vs.CitizenPortal.DataModel.Enums;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;
using static Vs.Rules.Core.TypeInference.InferenceResult;

namespace Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces
{
    public interface IFormElementData : IValidatableObject
    {
        CultureInfo Culture { get; set; }
        string ErrorText { get; }
        string HintText { get; set; }
        TypeEnum InferedType { get; set; }
        bool IsDisabled { get; set; }
        bool IsValid { get; set; }
        string Label { get; set; }
        string Name { get; set; }
        ElementSize Size { get; set; }
        string Value { get; set; }
        EventCallback<string> ValueChanged { get; set; }

        void CustomValidate(bool unobtrusive = false);
        void FillFromExecutionResult(IExecutionResult result, IContentController contentController);
    }
}