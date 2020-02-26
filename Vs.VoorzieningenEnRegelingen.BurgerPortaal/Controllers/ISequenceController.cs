using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers
{
    public interface ISequenceController
    {
        ISequence Sequence { get; set; }
        int CurrentStep { get; set; }
        int RequestStep { get; set; }
        ParseResult ParseResult { get; set; }
        ExecutionResult LastExecutionResult { get; set; }

        ExecuteRequest GetExecuteRequest(ParametersCollection parameters = null);
        ParseRequest GetParseRequest();
        void ExecuteStep(IParameter currentParameter);
    }
}
