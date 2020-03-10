using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects
{
    public class DisplayExecutionResult : IExecutionResult
    {

        public DisplayExecutionResult(IExecutionResult executionResult, IParametersCollection parameters)
        {
            IsError = executionResult.IsError;
            Message = executionResult.Message;
            Parameters = parameters;
            Questions = executionResult.Questions;
            Stacktrace = executionResult.Stacktrace;
        }

        public bool IsError { get; set; }

        public string Message { get; set; }

        public IParametersCollection Parameters { get; set; }

        public IQuestionArgs Questions { get; set; }

        public List<FlowExecutionItem> Stacktrace { get; set; }
    }
}
