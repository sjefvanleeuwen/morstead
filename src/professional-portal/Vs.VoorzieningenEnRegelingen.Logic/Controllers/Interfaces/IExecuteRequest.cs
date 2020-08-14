using Vs.Rules.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces
{
    public interface IExecuteRequest
    {
        string Config { get; set; }
        IParametersCollection Parameters { get; set; }
    }
}