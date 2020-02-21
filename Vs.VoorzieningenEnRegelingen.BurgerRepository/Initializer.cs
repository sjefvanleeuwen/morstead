using Microsoft.Extensions.DependencyInjection;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Controllers;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IStepController, StepController>();
        }
    }
}
