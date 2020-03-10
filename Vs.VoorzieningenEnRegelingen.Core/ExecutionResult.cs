using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ExecutionResult : IExecutionResult
    {
        public bool IsError { get; internal set; }
        public string Message { get; internal set; }
        public List<FlowExecutionItem> Stacktrace { get; }
        public IParametersCollection Parameters { get; }
        public IQuestionArgs Questions { get; set; }

        public ExecutionResult(ref IParametersCollection parameters)
        {
            Parameters = parameters;
            Stacktrace = new List<FlowExecutionItem>();
        }

        public static ExecutionResult NotExecutedBecauseOfParseError(ref IParametersCollection parameters) => new ExecutionResult(ref parameters) { IsError = true, Message = "Not Executed Because Of Parse Error" };
    }
}
