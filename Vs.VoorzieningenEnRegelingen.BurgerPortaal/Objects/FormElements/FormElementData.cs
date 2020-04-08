using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.Core.Extensions;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class FormElementData : IFormElementData
    {
        protected string value = string.Empty;

        public string Name { get; set; }
        public string Label { get; set; }
        public FormElementSize Size { get; set; }
        public string TagText { get; set; }
        public string HintText { get; set; }
        public IEnumerable<string> HintTextList { get; set; }
        public string ErrorText { get => !IsValid ? GetErrorText() : string.Empty; }
        public bool IsDisabled { get; set; } = false;
        public bool IsRequired { get; set; } = false;
        public bool IsValid { get; set; } = true;
        public TypeInference.InferenceResult.TypeEnum InferedType { get; set; }
        public CultureInfo Culture { get; set; } = new CultureInfo("nl-NL");
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

        public string ElementSize => Size.GetDescription();

        public IList<string> ErrorTexts = new List<string>();

        public virtual void Validate(bool unobtrusive = false)
        {
            //reset values
            IsValid = true;
            ErrorTexts = new List<string>();
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
            Label = contentController.GetText(parameterSemanticKey, FormElementContentType.Label, result.Parameters);
            TagText = contentController.GetText(parameterSemanticKey, FormElementContentType.Tag, result.Parameters);
            HintText = contentController.GetText(parameterSemanticKey, FormElementContentType.Hint, result.Parameters);
        }
    }
}
