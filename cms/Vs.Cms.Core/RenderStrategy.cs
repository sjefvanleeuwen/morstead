using Vs.Cms.Core.Interfaces;

namespace Vs.Cms.Core
{
    public class RenderStrategy : IRenderStrategy
    {
        private readonly ITemplateEngine templateEngine;
        private readonly IMarkupLanguage markupLanguage;

        public RenderStrategy(ITemplateEngine templateEngine, IMarkupLanguage markupLanguage)
        {
            this.templateEngine = templateEngine;
            this.markupLanguage = markupLanguage;
        }

        public string Render(string template, dynamic model)
        {
            return markupLanguage.Render(templateEngine.Render(template, model));
        }
    }
}
