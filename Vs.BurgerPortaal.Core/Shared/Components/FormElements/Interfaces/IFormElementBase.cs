using Microsoft.AspNetCore.Components;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;

namespace Vs.BurgerPortaal.Core.Shared.Components.FormElements.Interfaces
{
    public interface IFormElementBase
    {
        [CascadingParameter]
        public IFormElementData Data { get; set; }

        bool ShowElement { get; }
        bool HasInput { get; }

        void FillDataFromResult(IExecutionResult result, IContentController contentController);

        IFormElementBase GetFormElement(IExecutionResult result);
    }
}
