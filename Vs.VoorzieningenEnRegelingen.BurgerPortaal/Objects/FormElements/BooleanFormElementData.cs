using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class BooleanFormElementData : OptionsFormElementData, IBooleanFormElementData
    {
        public override void DefineOptions(IExecutionResult result)
        {
            foreach (var p in result.QuestionParameters)
            {
                //todo activate after texts have been restored
                //Options.Add(p.Name, FormElementHelper.GetParameterDisplayName(p.Name, result.Parameters));
                Options.Add(p.Name, p.Name);
            }
        }
    }
}
