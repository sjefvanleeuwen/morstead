using Microsoft.Extensions.DependencyInjection;
using Vs.ProfessionalPortal.Morstead.Client.Controllers;
using Vs.ProfessionalPortal.Morstead.Client.Controllers.Interfaces;

namespace Vs.ProfessionalPortal.Morstead.Client
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            OrleansConnectionProvider.StartClient();
            services.AddTransient<IYamlStorageController, YamlStorageController>();
        }
    }
}
