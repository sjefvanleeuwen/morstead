using Microsoft.Extensions.DependencyInjection;
using Vs.Definitions.Repositories;
using Vs.ProfessionalPortal.Morstead.Client.Controllers;

namespace Vs.ProfessionalPortal.Morstead.Client
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            OrleansConnectionProvider.StartClient();
            services.AddTransient<IFileRepository, YamlStorageRepository>();
        }
    }
}
