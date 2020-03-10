using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    public interface IExecuteRequest
    {
        string Config { get; set; }
        IParametersCollection Parameters { get; set; }
    }
}