using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Interface;
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

        public IEnumerable<ContentNode> ContentNodes { get; set; }

        public IStep Step { get; set; }

        public ExecutionResult(ref IParametersCollection parameters)
        {
            Parameters = parameters;
            Stacktrace = new List<FlowExecutionItem>();
        }

        public IEnumerable<IParameter> QuestionParameters => Questions?.Parameters?.GetAll() ?? new List<IParameter>();
        public IParameter QuestionFirstParameter => QuestionParameters.FirstOrDefault();

        public static ExecutionResult NotExecutedBecauseOfParseError(ref IParametersCollection parameters, ref IEnumerable<ContentNode> contentNodes) =>
            new ExecutionResult(ref parameters) { IsError = true, Message = "Not Executed Because Of Parse Error" };

        public string GetParameterSemanticKey(string parametername = null)
        {
            if (string.IsNullOrWhiteSpace(parametername))
            {
                parametername = QuestionFirstParameter?.Name;
            }
            var parameterSementicKey = $"{Step.SemanticKey}.{parametername}";
            var parameterSementicKeyKeuze = $"{Step.SemanticKey}.keuze.{parametername}";

            if (ContentNodes.Any(c => c.Parameter.SemanticKey == parameterSementicKey))
            {
                return parameterSementicKey;
            }
            if (ContentNodes.Any(c => c.Parameter.SemanticKey == parameterSementicKeyKeuze))
            {
                return parameterSementicKeyKeuze;
            }
            return Step.SemanticKey;
        }

        public TypeInference.InferenceResult.TypeEnum InferedType =>
            Questions?.Parameters?.GetAll()?.FirstOrDefault()?.Type ?? TypeInference.InferenceResult.TypeEnum.Unknown;
    }
}
