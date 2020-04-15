using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class ElementTester
    {
        readonly ITextFormElementData FieldText = new TextFormElementData
        {
            Label = "TextLabel",
            Name = "TextName",
            Value = "TextValue",
            HintText = "TextHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.String
        };

        readonly ITextFormElementData FieldTextArea = new TextFormElementData
        {
            Label = "TextAreaLabel",
            Name = "TextAreaName",
            Value = "TextAreaValue",
            HintText = "TextAreaHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.String
        };
    }
}
