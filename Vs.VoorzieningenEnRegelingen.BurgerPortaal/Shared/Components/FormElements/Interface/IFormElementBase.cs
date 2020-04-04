using Microsoft.AspNetCore.Components;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Interface
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

        void FillDataFromResult(IExecutionResult result, IContentController contentController);

        IFormElementBase GetFormElement(IExecutionResult result);
    }
}
