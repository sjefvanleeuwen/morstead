using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interfaces
{
    public interface ISequenceController
    {
        ISequence Sequence { get; }
        int CurrentStep { get; }
        int RequestStep { get; }
        IParseResult ParseResult { get; }
        IExecutionResult LastExecutionResult { get; }
        bool QuestionIsAsked { get; }
        bool HasRights { get; }

        IExecuteRequest GetExecuteRequest(IParametersCollection parameters = null);
        IParseRequest GetParseRequest();
        void IncreaseStep();
        void DecreaseStep();
        void ExecuteStep(IParametersCollection currentParameters);
        string GetSavedValue();
    }
}
