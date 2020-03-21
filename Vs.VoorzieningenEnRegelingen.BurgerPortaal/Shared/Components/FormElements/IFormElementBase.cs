using Microsoft.AspNetCore.Components;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public interface IFormElementBase
    {
        //Castcaded Data
        [CascadingParameter]
        public IFormElementData CascadedData { get; set; }

        //Same data but set, needed for testing purposes
        [Parameter]
        public IFormElementData Data { get; set; }

        [Parameter]
        public string Value { get; set; }

        bool ShowElement { get; }

        void FillDataFromResult(IExecutionResult result);
    }
}
