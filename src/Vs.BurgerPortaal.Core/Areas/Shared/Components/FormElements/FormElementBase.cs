﻿using Blazor.NLDesignSystem;
using Microsoft.AspNetCore.Components;
using Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements.Interfaces;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.CitizenPortal.DataModel.Enums;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements
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
                case TypeInference.InferenceResult.TypeEnum.String:
                    return new Text();
                case TypeInference.InferenceResult.TypeEnum.DateTime:
                case TypeInference.InferenceResult.TypeEnum.Period:
                case TypeInference.InferenceResult.TypeEnum.Step:
                case TypeInference.InferenceResult.TypeEnum.TimeSpan:
                case TypeInference.InferenceResult.TypeEnum.Unknown:
                default:
                    return this;
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
