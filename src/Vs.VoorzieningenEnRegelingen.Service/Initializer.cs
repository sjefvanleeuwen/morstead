using Microsoft.Extensions.DependencyInjection;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IAPIServiceController, APIServiceController>();
            Logic.Initializer.Initialize(services);
        }
    }
}
