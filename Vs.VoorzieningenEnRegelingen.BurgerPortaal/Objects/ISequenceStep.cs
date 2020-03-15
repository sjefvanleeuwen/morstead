using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects
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
