using Microsoft.AspNetCore.Components;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Interfaces
{
    public interface IFormElementBase
    {
        //Castcaded Data
        [CascadingParameter]
        public IFormElementData CascadedData { get; set; }

        //Same data but set, needed for testing purposes
        [Parameter]
        public IFormElementData Data { get; set; }

        bool ShowElement { get; }
        bool HasInput { get; }

        void FillDataFromResult(IExecutionResult result, IContentController contentController);

        IFormElementBase GetFormElement(IExecutionResult result);
    }
}
