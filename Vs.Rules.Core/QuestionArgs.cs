using Vs.Rules.Core.Interfaces;

namespace Vs.Rules.Core
{
    public class QuestionArgs : IQuestionArgs
    {
        public QuestionArgs(string sessionId, IParametersCollection parameters)
        {
            SessionId = sessionId ?? throw new System.ArgumentNullException(nameof(sessionId));
            Parameters = parameters ?? throw new System.ArgumentNullException(nameof(parameters));
        }

        public string SessionId { get; }
        public IParametersCollection Parameters { get; }
    }
}