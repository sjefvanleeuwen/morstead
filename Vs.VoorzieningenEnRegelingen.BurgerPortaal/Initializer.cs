using Microsoft.Extensions.DependencyInjection;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<ISequenceController, SequenceController>();
        }
    }
}
