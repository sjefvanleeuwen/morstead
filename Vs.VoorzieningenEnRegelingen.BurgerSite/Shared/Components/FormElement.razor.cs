using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerSite.Library.ExtensionMethods;

namespace Vs.VoorzieningenEnRegelingen.BurgerSite.Shared.Components
{
    public partial class FormElement
    {
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string Label { get; set; }
        [Parameter]
        public IEnumerable<FormElementLabel> Labels { get; set; }
        [Parameter]
        public string Value { get; set; }
        [Parameter]
        public IEnumerable<string> Values { get; set; }
        [Parameter]
        public FormElementType Type { get; set; }
        [Parameter]
        public Dictionary<string, string> Options { get; set; }
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
        

        private IEnumerable<string> _keys => Options.Keys;
        private string _type => Type.GetDescription();
        private string _size => Size.GetDescription();
        private bool _buttonIsIcon => !string.IsNullOrWhiteSpace(ButtonIcon);
        private bool _showTag => !string.IsNullOrWhiteSpace(TagText);
        private bool _showHint => !string.IsNullOrWhiteSpace(HintText);
        private bool _showError => !string.IsNullOrWhiteSpace(ErrorText);
    }

    public enum FormElementType
    {
        [Description("input__control--text")]
        Text,
        [Description("input__control--text")]
        TextArea,
        [Description("input__control--text")]
        Number,
        [Description("input__control--text")]
        Email,
        [Description("input__control--text")]
        Date,
        [Description("input__control--text")]
        Adress,
        [Description("input__control--select")]
        Select,
        [Description("input__control--search")]
        Search,
        [Description("input__control--select")]
        Checkbox,
        [Description("input__control--select")]
        Radio,
        [Description("input__control--select")]
        RadioShort
    }

    public enum FormElementSize
    {
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
