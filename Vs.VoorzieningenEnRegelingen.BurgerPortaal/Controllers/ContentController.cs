using Vs.Cms.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers
{
    public class ContentController : IContentController
    {
        private readonly IMarkupLanguage _markupLanguage;
        private readonly ITemplateEngine _templateEngine;

        public ContentController(ITemplateEngine templateEngine, IMarkupLanguage markupLanguage)
        {
            _templateEngine = templateEngine;
            _markupLanguage = markupLanguage;
        }

        public string GetText(FormElementContentType question, string semanticKey)
        {
            //todo implement
            return "Text";
        }
    }
}
