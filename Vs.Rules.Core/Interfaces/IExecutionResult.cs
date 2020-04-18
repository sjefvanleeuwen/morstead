using System.Collections.Generic;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Core.Interfaces
{
    public interface IExecutionResult
    {
        bool IsError { get; }
        string Message { get; }
        IParametersCollection Parameters { get; }
        IQuestionArgs Questions { get; set; }
        List<FlowExecutionItem> Stacktrace { get; }
        IStep Step { get; set; }
        IEnumerable<IParameter> QuestionParameters { get; }
        IParameter QuestionFirstParameter { get; }
        TypeInference.InferenceResult.TypeEnum InferedType { get; }
        IEnumerable<ContentNode> ContentNodes { get; set; }

        string GetParameterSemanticKey(string parametername = null);
    }
}