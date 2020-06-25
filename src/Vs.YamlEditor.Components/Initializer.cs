using Microsoft.Extensions.DependencyInjection;
using Vs.YamlEditor.Components.Controllers;
using Vs.YamlEditor.Components.Controllers.Interfaces;

namespace Vs.YamlEditor.Components
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddTransient<IMonacoController, MonacoController>();
            services.AddScoped<IValidationController, ValidationController>();
        }
    }
}
