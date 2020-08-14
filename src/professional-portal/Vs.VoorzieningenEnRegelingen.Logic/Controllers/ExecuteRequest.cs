using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Logic.Controllers
{
    public class ExecuteRequest : IExecuteRequest
    {
        public string Config { get; set; }
        public IParametersCollection Parameters { get; set; }
    }
}