using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interface;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class BooleanFormElementData : OptionsFormElementData, IListFormElementData
    {
        public override void DefineOptions(IExecutionResult result)
        {
            foreach (var p in result.Questions.Parameters.GetAll())
            {
                Options.Add(p.Name, FormElementHelper.GetParameterDisplayName(p.Name, result.Parameters));
            }
        }
    }
}
