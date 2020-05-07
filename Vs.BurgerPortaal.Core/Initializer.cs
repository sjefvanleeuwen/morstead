using Microsoft.Extensions.DependencyInjection;
using Vs.BurgerPortaal.Core.Controllers;
using Vs.BurgerPortaal.Core.Controllers.Interfaces;
using Vs.BurgerPortaal.Core.Objects;
using Vs.BurgerPortaal.Core.Objects.Interfaces;
using Vs.Cms.Core.Controllers;
using Vs.Cms.Core.Controllers.Interfaces;

namespace Vs.BurgerPortaal.Core
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<ISequenceController, SequenceController>();
            services.AddScoped<ISequence, Sequence>();
            services.AddScoped<IContentController, ContentController>();
            VoorzieningenEnRegelingen.Service.Initializer.Initialize(services);
            Cms.Core.Initializer.Initialize(services);
            Rules.Core.Initializer.Initialize(services);
        }
    }
}
