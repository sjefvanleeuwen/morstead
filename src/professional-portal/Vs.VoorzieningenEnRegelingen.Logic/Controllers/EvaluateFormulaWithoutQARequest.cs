using System.Collections.Generic;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Logic.Controllers
{
    public class EvaluateFormulaWithoutQARequest : IEvaluateFormulaWithoutQARequest
    {
        public string Config { get; set; }
        public IParametersCollection Parameters { get; set; }
        public IEnumerable<string> UnresolvedParameters { get; set; }
    }
}