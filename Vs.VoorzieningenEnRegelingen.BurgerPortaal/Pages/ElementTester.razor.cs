using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class ElementTester
    {
        readonly IFormElementData FieldText = new TextFormElementData
        {
            Label = "TextLabel",
            Name = "TextName",
            Value = "TextValue",
            HintText = "TextHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.String
        };

        private void ValidateText()
        {
            FieldText.CustomValidate();
        }

        readonly IFormElementData FieldTextArea = new TextFormElementData
        {
            Label = "TextAreaLabel",
            Name = "TextAreaName",
            Value = "TextAreaValue",
            HintText = "TextAreaHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.String
        };

        private void ValidateTextArea()
        {
            FieldTextArea.CustomValidate();
        }

        readonly IFormElementData FieldNumber = new NumericFormElementData
        {
            Label = "NumberLabel",
            Name = "NumberName",
            Value = "NumberValue",
            HintText = "NumberHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.Double
        };

        private void ValidateNumber()
        {
            FieldNumber.CustomValidate();
        }

        readonly IFormElementData FieldSelect = new ListFormElementData
        {
            Label = "SelectLabel",
            Name = "SelectName",
            Value = "SelectValue",
            HintText = "SelectHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.List,
            Options = new Dictionary<string, string>
            {
                { "", "MoetLeegZijn" },
                { "Optie1", "Optie1" },
                { "Optie2", "Optie2" },
                { "SelectValue", "Optie3" },
                { "Optie4", "Optie4" }
            }
        };

        private void ValidateSelect()
        {
            FieldSelect.CustomValidate();
        }
    }
}
