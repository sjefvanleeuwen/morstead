using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers
{
    public interface IExecuteRequest
    {
        string Config { get; set; }
        IParametersCollection Parameters { get; set; }
    }
}