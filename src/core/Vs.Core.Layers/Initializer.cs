using Microsoft.Extensions.DependencyInjection;
using Vs.Core.Layers.Controllers;
using Vs.Core.Layers.Controllers.Interfaces;

namespace Vs.Core.Layers
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<ILayerController, LayerController>();
            services.AddScoped<IYamlSourceController, YamlSourceController>();
        }
    }
}
