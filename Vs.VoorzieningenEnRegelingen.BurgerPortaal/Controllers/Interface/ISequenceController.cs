using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.Interface;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interface
{
    public interface ISequenceController
    {
        ISequence Sequence { get; }
        int CurrentStep { get; }
        int RequestStep { get; }
        IParseResult ParseResult { get; }
        IExecutionResult LastExecutionResult { get; }

        IExecuteRequest GetExecuteRequest(IParametersCollection parameters = null);
        IParseRequest GetParseRequest();
        void IncreaseStep();
        void DecreaseStep();
        void ExecuteStep(IParametersCollection currentParameters);
    }
}
