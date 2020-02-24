using Microsoft.Extensions.DependencyInjection;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;

namespace Vs.VoorzieningenEnRegelingen.Service
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IServiceController, ServiceController>();
        }
    }
}
