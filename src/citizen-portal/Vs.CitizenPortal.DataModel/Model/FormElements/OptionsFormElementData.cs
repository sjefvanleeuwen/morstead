using System.Collections.Generic;
using Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;

namespace Vs.CitizenPortal.DataModel.Model.FormElements
{
    public class OptionsFormElementData : FormElementData, IOptionsFormElementData
    {
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

        public override void FillFromExecutionResult(IExecutionResult result, IContentController contentController)
        {
            base.FillFromExecutionResult(result, contentController);
            DefineOptions(result, contentController);
        }

        public virtual void DefineOptions(IExecutionResult result, IContentController contentController)
        {
        }
    }
}
