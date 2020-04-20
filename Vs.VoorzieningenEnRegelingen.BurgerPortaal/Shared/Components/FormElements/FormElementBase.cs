using Microsoft.AspNetCore.Components;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public class FormElementBase : ComponentBase, IFormElementBase
    {
        [CascadingParameter]
        public IFormElementData Data { get; set; }      

        [Parameter]
        public virtual string Value { get => Data.Value; set => Data.Value = value; }

        public bool ShowElement => Data != null && !string.IsNullOrWhiteSpace(Data.Name);

        public virtual bool HasInput => false;

        public virtual void FillDataFromResult(IExecutionResult result, IContentController contentController)
        {
            //todo MPS write test
            Data = new FormElementData();
            if (result.InferedType != TypeInference.InferenceResult.TypeEnum.Unknown)
            {
                Data.FillFromExecutionResult(result, contentController);
            }
        }

        public IFormElementBase GetFormElement(IExecutionResult result)
        {
            switch (result.InferedType)
            {
                case TypeInference.InferenceResult.TypeEnum.Double:
                    return new Number();
                case TypeInference.InferenceResult.TypeEnum.Boolean:
                    return new Radio();
                case TypeInference.InferenceResult.TypeEnum.List:
                    return new Select();
                case TypeInference.InferenceResult.TypeEnum.DateTime:
                    return new Date();
                case TypeInference.InferenceResult.TypeEnum.Period:
                    return new DateRange();
                case TypeInference.InferenceResult.TypeEnum.String:
                    return new Text();
                case TypeInference.InferenceResult.TypeEnum.TimeSpan:
                case TypeInference.InferenceResult.TypeEnum.Unknown:
                default:
                    return this;
            }
        }
    }
}
