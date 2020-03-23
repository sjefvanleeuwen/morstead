using Vs.Cms.Core.Constants;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Interfaces;

namespace Vs.Cms.Core.Controllers
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
            //todo MPS implement
            return "Text";
        }
    }
}
