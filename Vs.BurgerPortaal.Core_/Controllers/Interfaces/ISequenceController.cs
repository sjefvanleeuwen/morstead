using System.Collections.Generic;
using Vs.BurgerPortaal.Core.Objects.Interfaces;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Service.Controllers.Interfaces;

namespace Vs.BurgerPortaal.Core.Controllers.Interfaces
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
        IParseResult GetParseResult();
        void IncreaseStep();
        void DecreaseStep();
        void ExecuteStep(IParametersCollection currentParameters);
        void FillUnresolvedParameters(ref IParametersCollection parameters, IEnumerable<string> unresolvedParameters);
        IEvaluateFormulaWithoutQARequest GetEvaluateFormulaWithoutQARequest(ref IParametersCollection parameters, IEnumerable<string> unresolvedParameters);
        string GetSavedValue();
    }
}
