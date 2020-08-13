using Blazor.NLDesignSystem;
using Microsoft.AspNetCore.Components;
using Vs.CitizenPortal.DataModel.Enums;
using Vs.CitizenPortal.DataModel.Model.FormElements;
using Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces;
using Vs.CitizenPortal.DataModel.Model.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;

namespace Vs.CitizenPortal.DataModel.Model
{
    public class FormElementBase : ComponentBase, IFormElementBase
    {
        [CascadingParameter]
        public IFormElementData Data { get; set; }

        [Parameter]
        public virtual string Value { get => Data.Value; set => Data.Value = value; }

        public InputSize InputSize => ElementSizeToInputSize(Data.Size);

        public virtual bool HasInput => false;
        public bool ShowElement => Data != null && !string.IsNullOrWhiteSpace(Data.Name);

        public virtual void FillDataFromResult(IExecutionResult result, IContentController contentController)
        {
            //todo MPS write test
            Data = new FormElementData();
            if (result.InferedType != TypeInference.InferenceResult.TypeEnum.Unknown)
            {
                Data.FillFromExecutionResult(result, contentController);
            }
        }

        private InputSize ElementSizeToInputSize(ElementSize size)
        {
            switch (size)
            {
                case ElementSize.ExtraSmall:
                    return InputSize.ExtraSmall;
                case ElementSize.Small:
                    return InputSize.Small;
                case ElementSize.Large:
                    return InputSize.Large;
                case ElementSize.ExtraLarge:
                    return InputSize.ExtraLarge;
                case ElementSize.Undefined:
                case ElementSize.Medium:
                default:
                    return InputSize.Medium;
            }
        }
    }
}
