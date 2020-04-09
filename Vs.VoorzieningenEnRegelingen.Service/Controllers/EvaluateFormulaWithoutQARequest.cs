using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    public class EvaluateFormulaWithoutQARequest : IEvaluateFormulaWithoutQARequest
    {
        public string Config { get; set; }
        public IParametersCollection Parameters { get; set; }
        public IEnumerable<string> UnresolvedParameters { get; set; }
    }
}