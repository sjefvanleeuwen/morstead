using Microsoft.Extensions.DependencyInjection;
using Vs.Rules.Core.Interfaces;

namespace Vs.Rules.Core
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IYamlScriptController, YamlScriptController>();
        }
    }
}
