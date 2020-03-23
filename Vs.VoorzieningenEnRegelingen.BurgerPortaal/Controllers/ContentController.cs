using Vs.Cms.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interfaces;

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
    }
}
