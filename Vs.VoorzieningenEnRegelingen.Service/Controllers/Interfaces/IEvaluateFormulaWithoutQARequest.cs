using System.Collections.Generic;
using Vs.Rules.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces
{
    public interface IEvaluateFormulaWithoutQARequest
    {
        string Config { get; set; }
        IParametersCollection Parameters { get; set; }
        IEnumerable<string> UnresolvedParameters { get; set; }
    }
}