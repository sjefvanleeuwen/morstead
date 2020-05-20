using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
    /// <summary>
    /// Returns all the state needed to process a yaml execution result on the client.
    /// </summary>
    public abstract class ExecuteRuleYamlResponse
    {
        public IEnumerable<ServerParameter> ServerParameters { get; set; }

        public ExecuteRuleYamlResultTypes ExecutionResult { get; set; }
    }

    /// <summary>
    /// Possible Execution result types for Yaml Rule Execution
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ExecuteRuleYamlResultTypes
    {
        Ok,
        NotFound,
        SyntaxError,
    }
}

