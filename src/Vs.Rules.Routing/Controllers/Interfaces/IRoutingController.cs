using System.Threading.Tasks;
using Vs.Rules.Routing.Model.Interfaces;

namespace Vs.Rules.Routing.Controllers.Interfaces
{
    public interface IRoutingController
    {
        IRoutingConfiguration RoutingConfiguration { get; }
        Task Initialize(string routingYaml = null);
        Task<string> GetParameterValue(string missingParameterName);
    }
}