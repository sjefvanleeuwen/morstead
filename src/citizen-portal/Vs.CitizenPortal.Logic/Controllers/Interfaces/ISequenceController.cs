using System.Collections.Generic;
using System.Threading.Tasks;
using Vs.CitizenPortal.Logic.Objects.Interfaces;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;

namespace Vs.CitizenPortal.Logic.Controllers.Interfaces
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
        Task<IParseResult> GetParseResult();
        void IncreaseStep();
        void DecreaseStep();
        Task ExecuteStep(IParametersCollection currentParameters);
        Task<IParametersCollection> FillUnresolvedParameters(IParametersCollection parameters, IEnumerable<string> unresolvedParameters);
        IEvaluateFormulaWithoutQARequest GetEvaluateFormulaWithoutQARequest(ref IParametersCollection parameters, IEnumerable<string> unresolvedParameters);
        string GetSavedValue();
    }
}
