using Microsoft.AspNetCore.Components;
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
    }
}
