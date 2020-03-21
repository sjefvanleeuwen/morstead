using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Vs.Core.Extensions;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class FormElementData : IFormElementData
    {
        private string _value = string.Empty;

        public string Name { get; set; }
        public string Label { get; set; }
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                {
                    return;
                }
                _value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(value);
                }
            }
        }
        public EventCallback<string> ValueChanged { get; set; }
        public FormElementSize Size { get; set; }
        public string TagText { get; set; }
        public string HintText { get; set; }
        public IEnumerable<string> HintTextList { get; set; }
        public string ErrorText { get => !IsValid ? _errorText : string.Empty; set => _errorText = value; }
        public bool IsDisabled { get; set; } = false;
        public bool IsRequired { get; set; } = false;
        public bool IsValid { get; set; } = true;

        private string _errorText;

        public TypeInference.InferenceResult.TypeEnum InferedType { get; set; }
        public string ElementSize => Size.GetDescription();

        public virtual void Validate(bool unobtrusive = false)
        {
            //reset values
            IsValid = true;
            _errorText = string.Empty;

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
                _errorText = "Vul een waarde in.";
            }

            return valid;
        }
    }
}
