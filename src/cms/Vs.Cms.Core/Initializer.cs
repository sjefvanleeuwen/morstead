using Microsoft.Extensions.DependencyInjection;
using Vs.Cms.Core.Interfaces;
using Vs.Cms.Core.Objects;
using Vs.Cms.Core.Objects.Interfaces;

namespace Vs.Cms.Core
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddSingleton<ICultureContentContainer, CultureContentContainer>();
            services.AddSingleton<IContentHandler, ContentHandler>();
            services.AddSingleton<IRenderStrategy, RenderStrategy>();
            services.AddSingleton<ITemplateEngine, Liquid>();
            services.AddSingleton<IContentFilter, HtmlContentFilter>();
            services.AddSingleton<IMarkupLanguage, Markdown>();
        }
    }
}
