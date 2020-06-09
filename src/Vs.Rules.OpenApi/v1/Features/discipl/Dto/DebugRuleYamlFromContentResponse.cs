using Vs.Rules.OpenApi.v1.Dto;

namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
    /// <summary>
    /// Returns all the information needed to debug a yaml rule.
    /// </summary>
    public class DebugRuleYamlFromContentResponse
    {
        /// <summary>
        /// Shows the execution status of the rule.
        /// </summary>
        /// <value>
        /// The execution status.
        /// </value>
        public ExecuteRuleYamlResultTypes ExecutionStatus { get; set; }
        /// <summary>
        /// Gets the parsed yaml result (before execution) these results contain debug information if needed.
        /// </summary>
        /// <value>
        /// The parse result.
        /// </value>
        public ParseResult ParseResult { get; set; }
    }
}

