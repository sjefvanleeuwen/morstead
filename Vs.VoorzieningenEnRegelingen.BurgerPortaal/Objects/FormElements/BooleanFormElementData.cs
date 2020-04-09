using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class BooleanFormElementData : OptionsFormElementData, IBooleanFormElementData
    {
        public override void DefineOptions(IExecutionResult result, IContentController contentController)
        {
            foreach (var p in result.QuestionParameters)
            {
                Options.Add(p.Name, contentController.GetText(
                    result.GetParameterSemanticKey(p.Name), FormElementContentType.Description, result.GetParameterSemanticKey(p.Name)));
            }
        }
    }
}
