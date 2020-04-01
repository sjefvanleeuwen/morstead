using System.ComponentModel;

namespace Vs.Cms.Core.Enums
{
    public enum FormElementContentType
    {
        [Description("vraag")]
        Question,
        [Description("titel")]
        Title,
        [Description("tekst")]
        Description,
        [Description("label")]
        Label,
        [Description("tag")]
        Tag,
        [Description("hint")]
        Hint,
        [Description("optiontext")]
        Option
    }
}
