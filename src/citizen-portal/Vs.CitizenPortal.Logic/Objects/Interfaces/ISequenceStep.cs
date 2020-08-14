using System.Collections.Generic;
using Vs.Rules.Core.Model;

namespace Vs.CitizenPortal.Logic.Objects.Interfaces
{
    public interface ISequenceStep
    {
        public int Key { get; set; }
        string SemanticKey { get; set; }
        string ParameterName { get; set; }
        IEnumerable<string> ValidParameterNames { get; set; }

        bool HasMultipleValidParameterNames();
        bool IsMatch(IParameter p);
    }
}
