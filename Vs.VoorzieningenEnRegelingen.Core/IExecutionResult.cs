using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public interface IExecutionResult
    {
        bool IsError { get; }
        string Message { get; }
        IParametersCollection Parameters { get; }
        IQuestionArgs Questions { get; set; }
        List<FlowExecutionItem> Stacktrace { get; }
    }
}