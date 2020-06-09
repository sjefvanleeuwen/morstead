using System.Collections.Generic;
using Vs.Rules.OpenApi.v1.Dto;

namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
    /// <summary>
    /// Returns all the state needed to process a yaml execution result on the client.
    /// </summary>
    public abstract class ExecuteRuleYamlResponse
    {
        /// <summary>
        /// Gets the questions that the server needs to be answered by the client to continue rule execution.
        /// </summary>
        /// <value>
        /// The questions.
        /// </value>
        public IEnumerable<ServerParameter> Questions { get; set; }
        /// <summary>
        /// Gets the parameters calculated by the server, which the client can use.
        /// </summary>
        /// <value>
        /// The server parameters.
        /// </value>
        public IEnumerable<ServerParameter> ServerParameters { get; set; }
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

