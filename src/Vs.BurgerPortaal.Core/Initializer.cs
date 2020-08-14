using Microsoft.Extensions.DependencyInjection;
using Vs.CitizenPortal.Logic.Controllers;
using Vs.CitizenPortal.Logic.Controllers.Interfaces;
using Vs.CitizenPortal.Logic.Objects;
using Vs.CitizenPortal.Logic.Objects.Interfaces;
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
            VoorzieningenEnRegelingen.Logic.Initializer.Initialize(services);
            Vs.Core.Layers.Initializer.Initialize(services);
            Cms.Core.Initializer.Initialize(services);
            Rules.Core.Initializer.Initialize(services);
            CitizenPortal.Logic.Initializer.Initialize(services);
        }
    }
}
