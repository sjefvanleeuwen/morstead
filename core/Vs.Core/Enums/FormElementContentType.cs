using System.ComponentModel;

namespace Vs.Core.Enums
{
    public enum FormElementContentType
    {
        [Description("vraag")]
        Question,
        [Description("titel")]
        Title,
        [Description("ondertitel")]
        SubTitle,
        [Description("tekst")]
        Description,
        [Description("label")]
        Label,
        [Description("labelstart")]
        LabelStart,
        [Description("labelend")]
        LabelEnd,
        [Description("hint")]
        Hint
    }
}
