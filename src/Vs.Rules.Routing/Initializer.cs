using Microsoft.Extensions.DependencyInjection;
using Vs.Rules.Routing.Controllers;
using Vs.Rules.Routing.Controllers.Interfaces;

namespace Vs.Rules.Routing
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IRoutingController, RoutingController>();
        }
    }
}
