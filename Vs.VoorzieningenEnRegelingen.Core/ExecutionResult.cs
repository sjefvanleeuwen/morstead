using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ExecutionResult
    {
        public bool IsError { get; internal set; }
        public string Message { get; internal set; }
        public List<FlowExecutionItem> Stacktrace { get; }

        public ExecutionResult()
        {
            Stacktrace = new List<FlowExecutionItem>();
        }
    }
}
