using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class TextArea
    {
        private ITextFormElementData _data => Data as ITextFormElementData;

        public override bool HasInput => true;

        public override void FillDataFromResult(IExecutionResult result, IContentController contentController)
        {
            Data = new TextFormElementData();
            Data.FillFromExecutionResult(result, contentController);
        }
    }
}
