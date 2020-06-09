using Vs.Cms.Core.Interfaces;

namespace Vs.Cms.Core
{
    public class RenderStrategy : IRenderStrategy
    {
        private readonly ITemplateEngine templateEngine;
        private readonly IMarkupLanguage markupLanguage;
        private readonly IContentFilter contentFilter;

        public RenderStrategy(ITemplateEngine templateEngine, IMarkupLanguage markupLanguage, IContentFilter contentFilter)
        {
            this.templateEngine = templateEngine;
            this.markupLanguage = markupLanguage;
            this.contentFilter = contentFilter;
        }

        public string Render(string template, dynamic model)
        {
            return contentFilter.Filter(markupLanguage.Render(templateEngine.Render(template, model)));
        }
    }
}
