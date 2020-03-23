using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interfaces
{
    interface IContentController
    {
        string GetText(FormElementContentType question, string semanticKey);
    }
}
