using Microsoft.Extensions.DependencyInjection;

namespace Vs.VoorzieningenEnRegelingen.Site
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            YamlEditor.Components.Initializer.Initialize(services);
            ProfessionalPortal.Morstead.Client.Initializer.Initialize(services);
        }
    }
}
