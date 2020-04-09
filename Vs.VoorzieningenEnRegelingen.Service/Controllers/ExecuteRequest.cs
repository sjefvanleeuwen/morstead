using Vs.VoorzieningenEnRegelingen.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    public class ExecuteRequest : IExecuteRequest
    {
        public string Config { get; set; }
        public IParametersCollection Parameters { get; set; }
    }
}