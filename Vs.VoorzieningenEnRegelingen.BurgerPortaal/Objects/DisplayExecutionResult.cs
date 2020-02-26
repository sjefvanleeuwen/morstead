using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects
{
    public class DisplayExecutionResult : IExecutionResult
    {

        public DisplayExecutionResult(ExecutionResult executionResult, ParametersCollection parameters)
        {
            IsError = executionResult.IsError;
            Message = executionResult.Message;
            Parameters = parameters;
            Questions = executionResult.Questions;
            Stacktrace = executionResult.Stacktrace;
        }

        public bool IsError { get; set; }

        public string Message { get; set; }

        public ParametersCollection Parameters { get; set; }

        public QuestionArgs Questions { get; set; }

        public List<FlowExecutionItem> Stacktrace { get; set; }
    }
}
