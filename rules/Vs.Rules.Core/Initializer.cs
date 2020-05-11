using Microsoft.Extensions.DependencyInjection;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Routing;

namespace Vs.Rules.Core
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IYamlScriptController, YamlScriptController>();
            Routing.Initializer.Initialize(services);
        }
    }
}
