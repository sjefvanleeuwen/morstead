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
            services.AddScoped<IContentController, ContentController>();
            services.AddScoped<IRenderStrategy, RenderStrategy>();
            services.AddScoped<ITemplateEngine, Liquid>();
            services.AddScoped<IMarkupLanguage, Markdown>();
        }
    }
}
