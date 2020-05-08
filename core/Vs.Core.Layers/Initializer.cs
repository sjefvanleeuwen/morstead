using Microsoft.Extensions.DependencyInjection;
using Vs.Core.Layers.Controllers;
using Vs.Core.Layers.Controllers.Interfaces;

namespace Vs.Cms.Core
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddSingleton<ILayerController, LayerController>();
        }
    }
}
