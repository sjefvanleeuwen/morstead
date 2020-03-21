using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class ListFormElementData : OptionsFormElementData, IListFormElementData
    {
        public override void FillFromExecutionResult(IExecutionResult result)
        {
            base.FillFromExecutionResult(result);

            Size = FormElementSize.Large;
        }
    }
}
