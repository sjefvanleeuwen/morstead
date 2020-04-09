using Microsoft.Extensions.DependencyInjection;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IYamlScriptController, YamlScriptController>();
        }
    }
}
