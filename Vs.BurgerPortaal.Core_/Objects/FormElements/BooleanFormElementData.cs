using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Core.Enums;
using Vs.Rules.Core.Interfaces;

namespace Vs.BurgerPortaal.Core.Objects.FormElements
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
