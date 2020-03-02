using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers
{
    public interface ISequenceController
    {
        ISequence Sequence { get; }
        int CurrentStep { get; }
        int RequestStep { get; }
        ParseResult ParseResult { get; }
        ExecutionResult LastExecutionResult { get; }

        ExecuteRequest GetExecuteRequest(ParametersCollection parameters = null);
        ParseRequest GetParseRequest();
        void IncreaseStep();
        void DecreaseStep();
        void ExecuteStep(ParametersCollection currentParameters);
    }
}
