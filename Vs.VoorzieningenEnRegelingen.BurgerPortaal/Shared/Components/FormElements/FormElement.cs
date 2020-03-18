using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vs.Core.Extensions;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class FormElement : ComponentBase, IFormElement
    {
        private string _value;

        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string Label { get; set; }
        [Parameter]
        public IEnumerable<FormElementLabel> Labels { get; set; }
        [Parameter]
        public virtual string Value
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
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        [Parameter]
        public IEnumerable<string> Values { get; set; }
        [Parameter]
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();
        [Parameter]
        public string ButtonIcon { get; set; }
        [Parameter]
        public string ButtonText { get; set; }
        [Parameter]
        public FormElementSize Size { get; set; }
        [Parameter]
        public string TagText { get; set; }
        [Parameter]
        public string HintText { get; set; }
        [Parameter]
        public IEnumerable<string> HintTextList { get; set; }
        [Parameter]
        public string ErrorText { get; set; }
        [Parameter]
        public bool IsDisabled { get; set; } = false;
        [Parameter]
        public bool IsRequired { get; set; } = false;
        [Parameter]
        public bool IsValid { get; set; } = true;

        public TypeInference.InferenceResult.TypeEnum InferedType { get; set; }

        public bool ShowElement => !string.IsNullOrWhiteSpace(Name);

        protected string ElementSize => Size.GetDescription();

        public bool Validate(bool unobtrusive = false)
        {
            //reset values
            IsValid = true;
            ErrorText = null;

            string errorText = null;
            var valid =
                ValidateValueIsSet(out errorText)
                && (InferedType != TypeInference.InferenceResult.TypeEnum.Double || ValidateValueIsValidNumber(out errorText));
            if (!unobtrusive)
            {
                IsValid = valid;
                ErrorText = errorText;
            }
            return valid;
        }

        private bool ValidateValueIsSet(out string errorText)
        {
            errorText = string.Empty;
            var valid = !string.IsNullOrWhiteSpace(Value);
            if (!valid)
            {
                errorText = "Vul een waarde in.";
            }

            return valid;
        }

        private bool ValidateValueIsValidNumber(out string errorText)
        {
            errorText = string.Empty;
            var chars = new List<char>(Value.ToCharArray());
            var validChars = new List<char>("1234567890,".ToCharArray());
            foreach(var c in Value)
            {
                if (!validChars.Contains(c))
                {
                    errorText = "Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.";
                    return false;
                }
            }
            if (chars.Where(c => c == ',').Count() > 1)
            {
                errorText = "Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.";
                return false;
            }
            var parts = Value.Split(',');
            if (parts.Count() == 2 && parts[1].Length != 2)
            {
                errorText = "Typ twee cijfers achter de komma.";
                return false;
            }

            return true;
        }
    }

    public enum FormElementSize
    {
        [Description("")]
        Default,
        [Description("input__control--xs")]
        ExtraSmall,
        [Description("input__control--s")]
        Small,
        [Description("input__control--m")]
        Medium,
        [Description("input__control--l")]
        Large,
        [Description("input__control--xl")]
        ExtraLarge,
        [Description("input__control--two-characters")]
        Two,
        [Description("input__control--four-characters")]
        Four,
        [Description("input__control--five-characters")]
        Five,
        [Description("input__control--six-characters")]
        Six
    }

    public class FormElementLabel
    {
        public string Text { get; set; }
        public string Title { get; set; }
    }
}
