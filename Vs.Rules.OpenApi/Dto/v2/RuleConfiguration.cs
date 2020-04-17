using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vs.Rules.OpenApi.Dto.v2
{
    public class RuleConfiguration
    {
        public Uri RuleYaml { get; set; }
        public Uri? ContentYaml { get; set; }
    }
}
