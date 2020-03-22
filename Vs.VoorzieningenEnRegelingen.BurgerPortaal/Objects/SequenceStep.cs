using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.Interface;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects
{
    public class SequenceStep : ISequenceStep
    {
        public int Key { get; set; }
        public string SemanticKey { get; set; }
        public string ParameterName { get; set; }
        public IEnumerable<string> ValidParameterNames { get; set; }

        public bool HasMultipleValidParameterNames() => ValidParameterNames?.ToList()?.Any() ?? false;

        public bool IsMatch(IParameter parameter)
        {
            return parameter != null &&
                (ParameterName == parameter.Name ||
                    (ValidParameterNames != null && ValidParameterNames.Contains(parameter.Name)))
                //&& parameter.Key == Key
                ;
        }
    }
}
