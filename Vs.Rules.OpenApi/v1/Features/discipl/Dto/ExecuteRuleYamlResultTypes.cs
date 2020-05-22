using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
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

