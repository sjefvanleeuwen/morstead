using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ExecutionResult : IExecutionResult
    {
        public bool IsError { get; internal set; }
        public string Message { get; internal set; }
        public List<FlowExecutionItem> Stacktrace { get; }
        public IParametersCollection Parameters { get; }
        public IQuestionArgs Questions { get; set; }

        //todo MPS implement
        public string SemanticKey => "TheSemanticKey";

        public ExecutionResult(ref IParametersCollection parameters)
        {
            Parameters = parameters;
            Stacktrace = new List<FlowExecutionItem>();
        }

        public IEnumerable<IParameter> QuestionParameters => Questions?.Parameters?.GetAll() ?? new List<IParameter>();

        public static ExecutionResult NotExecutedBecauseOfParseError(ref IParametersCollection parameters) =>
            new ExecutionResult(ref parameters) { IsError = true, Message = "Not Executed Because Of Parse Error" };

        public TypeInference.InferenceResult.TypeEnum InferedType =>
            Questions?.Parameters?.GetAll()?.FirstOrDefault()?.Type ?? TypeInference.InferenceResult.TypeEnum.Unknown;

    }
}
