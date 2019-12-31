using System;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class FlowExecutionItem
    {
        public FlowExecutionItem(Step step, Exception exception = null)
        {
            Step = step ?? throw new ArgumentNullException(nameof(step));
            Exception = exception;
        }

        public Step Step { get; }
        public Exception Exception { get; }
    }
}
