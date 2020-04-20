using Microsoft.AspNetCore.Components;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Interfaces
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
