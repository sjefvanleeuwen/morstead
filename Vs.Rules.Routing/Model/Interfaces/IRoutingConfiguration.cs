using System.Collections.Generic;
using Vs.Core.Layers.Model.Interfaces;

namespace Vs.Rules.Routing.Model.Interfaces
{
    public interface IRoutingConfiguration
    {
        IEnumerable<RoutingParameter> Parameters { get; set; }
        IGuidanceInformation Stuurinformatie { get; set; }
    }
}