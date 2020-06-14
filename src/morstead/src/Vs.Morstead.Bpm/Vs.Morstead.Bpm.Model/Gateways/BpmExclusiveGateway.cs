using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Vs.Morstead.Bpm.Core;
using Vs.Morstead.Bpm.Model.Tasks;

namespace Vs.Morstead.Bpm.Model.Gateways
{
    public class BpmExclusiveGateway : GatewayBase
    {
        public BpmExclusiveGateway(JToken gateway) : base(gateway)
        {
           
        }
    }
}
