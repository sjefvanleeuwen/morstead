using Vs.VoorzieningenEnRegelingen.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces
{
    public interface IExecuteRequest
    {
        string Config { get; set; }
        IParametersCollection Parameters { get; set; }
    }
}