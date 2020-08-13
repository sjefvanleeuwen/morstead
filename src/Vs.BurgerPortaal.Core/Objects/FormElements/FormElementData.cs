using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.CitizenPortal.DataModel.Enums;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Core.Enums;
using Vs.Rules.Core.Interfaces;
using static Vs.Rules.Core.TypeInference.InferenceResult;

namespace Vs.BurgerPortaal.Core.Objects.FormElements
{
    public class FormElementData : IFormElementData
    {
        protected string value = string.Empty;
        public CultureInfo Culture { get; set; } = new CultureInfo("nl-NL");
        public string ErrorText { get => !IsValid ? GetErrorText() : string.Empty; }
        public IList<string> ErrorTexts = new List<string>();
        public string HintText { get; set; }
        public TypeEnum InferedType { get; set; }
        public bool IsDisabled { get; set; } = false;
        public bool IsValid { get; set; } = true;
        public string Label { get; set; }
        public string Name { get; set; }
        public ElementSize Size { get; set; }

        public virtual string Value
        {
            get { return value; }
            set
            {
                if (this.value == value)
                {
                    return;
                }
                this.value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(value);
                }
            }
        }
        public virtual EventCallback<string> ValueChanged { get; set; }

        public virtual void CustomValidate(bool unobtrusive = false)
        {
            IsValid = true;
            ErrorTexts = new List<string>();

            var valid = ValidateValueIsSet();

            if (!unobtrusive)
            {
                IsValid = valid;
            }
        }

        private bool ValidateValueIsSet()
        {
            var valid = !string.IsNullOrWhiteSpace(Value);
            if (!valid)
            {
                ErrorTexts.Add("Vul een waarde in.");
            }

            return valid;
        }

        private string GetErrorText()
        {
            return string.Join(Environment.NewLine, ErrorTexts);
        }

        public virtual void FillFromExecutionResult(IExecutionResult result, IContentController contentController)
        {
            //todo MPS write test for this
            InferedType = result.InferedType;
            Name = result.QuestionFirstParameter.Name;
            var parameterSemanticKey = result.GetParameterSemanticKey();
            Label = contentController.GetText(parameterSemanticKey, FormElementContentType.Label);
            HintText = contentController.GetText(parameterSemanticKey, FormElementContentType.Hint);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsValid)
            {
                foreach (var errorText in ErrorTexts)
                {
                    yield return new ValidationResult(errorText, new[] { "Value" });
                }
            }
        }
    }
}
