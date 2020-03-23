using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public interface IExecutionResult
    {
        bool IsError { get; }
        string Message { get; }
        IParametersCollection Parameters { get; }
        IQuestionArgs Questions { get; set; }
        List<FlowExecutionItem> Stacktrace { get; }
        string SemanticKey { get; }
        IEnumerable<IParameter> QuestionParameters { get; }
        TypeInference.InferenceResult.TypeEnum InferedType { get; }
    }
}