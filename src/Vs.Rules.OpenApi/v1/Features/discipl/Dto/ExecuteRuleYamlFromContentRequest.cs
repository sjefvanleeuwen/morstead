namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
    /// <summary>
    /// Execute a rule by passing a yaml and evaluating it against a collection of client parameters.
    /// </summary>
    /// <seealso cref="Vs.Rules.OpenApi.v1.Features.discipl.Dto.ExecuteRuleYamlRequestBase" />
    public class ExecuteRuleYamlFromContentRequest : ExecuteRuleYamlRequestBase
    {
        public string Yaml { get; set; }
    }
}