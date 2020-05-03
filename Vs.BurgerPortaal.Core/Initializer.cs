using Microsoft.Extensions.DependencyInjection;
using Vs.BurgerPortaal.Core.Controllers;
using Vs.BurgerPortaal.Core.Controllers.Interfaces;
using Vs.BurgerPortaal.Core.Objects;
using Vs.BurgerPortaal.Core.Objects.Interfaces;

namespace Vs.BurgerPortaal.Core
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<ISequenceController, SequenceController>();
            services.AddScoped<ISequence, Sequence>();

            VoorzieningenEnRegelingen.Service.Initializer.Initialize(services);
            Cms.Core.Initializer.Initialize(services);
            Rules.Core.Initializer.Initialize(services);
        }
    }
}
