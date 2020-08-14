using Microsoft.Extensions.DependencyInjection;

namespace Vs.CitizenPortal.Logic
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            VoorzieningenEnRegelingen.Logic.Initializer.Initialize(services);
        }
    }
}
