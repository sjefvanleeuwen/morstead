using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    public class ExecuteRequest
    {
        public string Config { get; set; }
        public ParametersCollection Parameters { get; set; }
    }
}