using Microsoft.Extensions.DependencyInjection;
using Vs.VoorzieningenEnRegelingen.Site.Controllers;
using Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IMenu, Menu>();
            services.AddScoped<IEditorTabController, EditorTabController>();
            ProfessionalPortal.Morstead.Client.Initializer.Initialize(services);
            services.AddScoped<IYamlValidationController, YamlValidationController>();
        }
    }
}
