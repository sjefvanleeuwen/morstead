using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class BooleanFormElementData : OptionsFormElementData, IBooleanFormElementData
    {
        public override void DefineOptions(IExecutionResult result, IContentController contentController)
        {
            foreach (var p in result.QuestionParameters)
            {
                //todo MPS activate after texts have been restored
                Options.Add(p.Name, contentController.GetText(result.SemanticKey, Cms.Core.Enums.FormElementContentType.Options, p.Name));
            }
        }
    }
}
