using System.ComponentModel;

namespace Vs.Cms.Core.Enums
{
    public enum FormElementContentType
    {
        [Description("vraag")]
        Question,
        [Description("titel")]
        Title,
        [Description("subtitel")]
        SubTitle,
        [Description("tekst")]
        Description,
        [Description("label")]
        Label,
        [Description("hint")]
        Hint
    }
}
