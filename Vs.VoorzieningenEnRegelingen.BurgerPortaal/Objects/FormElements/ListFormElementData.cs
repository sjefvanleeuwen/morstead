using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interface;
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

        public override void DefineOptions(IExecutionResult result)
        {
            (result.Questions.Parameters.GetAll().First().Value as List<object>)?.ForEach(v => Options.Add(v.ToString(), v.ToString()));
        }
    }
}
