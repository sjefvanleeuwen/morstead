using System;
using Vs.Core.Diagnostics;

namespace Vs.Rules.Core.Model
{

    public class StuurInformatie
    {
        public DebugInfo DebugInfo { get; internal set; }
        public string Onderwerp { get; internal set; }
        public string Organisatie { get; internal set; }
        public string Type { get; internal set; }
        public string Domein { get; internal set; }
        public string Versie { get; internal set; }
        public string Status { get; internal set; }
        public string Jaar { get; internal set; }
        public string Bron { get; internal set; }
    }
}
