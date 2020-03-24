using Microsoft.Extensions.DependencyInjection;
using Vs.Cms.Core.Controllers;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Interfaces;

namespace Vs.Cms.Core
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddSingleton<IContentController, ContentController>();
            services.AddSingleton<IRenderStrategy, RenderStrategy>();
            services.AddSingleton<ITemplateEngine, Liquid>();
            services.AddSingleton<IContentFilter, HtmlContentFilter>();
            services.AddSingleton<IMarkupLanguage, Markdown>();
        }
    }
}
