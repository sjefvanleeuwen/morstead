using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    public class ExecuteRequest : IExecuteRequest
    {
        public string Config { get; set; }
        public IParametersCollection Parameters { get; set; }
    }
}