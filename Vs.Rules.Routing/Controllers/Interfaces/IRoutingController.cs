using Vs.Rules.Routing.Model.Interfaces;

namespace Vs.Rules.Routing.Controllers.Interfaces
{
    public interface IRoutingController
    {
        IRoutingConfiguration RoutingConfiguration { get; set; }
    }
}