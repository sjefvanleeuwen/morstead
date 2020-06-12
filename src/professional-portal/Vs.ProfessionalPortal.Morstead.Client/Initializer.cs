using Microsoft.Extensions.DependencyInjection;

namespace Vs.ProfessionalPortal.Morstead.Client
{
    public static class Initializer
    {
        public async static void Initialize(IServiceCollection services)
        {
            services.AddSingleton(await ProgramExtension.StartClient());
        }
    }
}
