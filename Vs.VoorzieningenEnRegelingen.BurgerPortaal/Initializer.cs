using Microsoft.Extensions.DependencyInjection;
using Vs.Cms.Core;
using Vs.Cms.Core.Controllers;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<ISequenceController, SequenceController>();
            services.AddScoped<ISequence, Sequence>();
            services.AddScoped<IContentController, ContentController>();
            services.AddScoped<ITemplateEngine, Liquid>();
            services.AddScoped<IMarkupLanguage, Markdown>();
            Service.Initializer.Initialize(services);
        }
    }
}
