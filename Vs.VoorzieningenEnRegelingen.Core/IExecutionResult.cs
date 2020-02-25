using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public interface IExecutionResult
    {
        bool IsError { get; }
        string Message { get; }
        ParametersCollection Parameters { get; }
        QuestionArgs Questions { get; set; }
        List<FlowExecutionItem> Stacktrace { get; }
    }
}