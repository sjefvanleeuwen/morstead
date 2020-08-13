using Microsoft.AspNetCore.Components;
using Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;

namespace Vs.CitizenPortal.DataModel.Model.Interfaces
{
    public interface IFormElementBase
    {
        [CascadingParameter]
        public IFormElementData Data { get; set; }

        bool ShowElement { get; }
        bool HasInput { get; }

        void FillDataFromResult(IExecutionResult result, IContentController contentController);
    }
}
