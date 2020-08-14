using Microsoft.Extensions.DependencyInjection;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Logic
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IServiceController, ServiceController>();
        }
    }
}
