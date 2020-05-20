namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
    /// <summary>
    /// Abstract implementation for rule execution.
    /// </summary>
    public abstract class ExecuteRuleYamlRequestBase
    {
        /// <summary>
        /// Gets or sets the client parameters used to evaluate the rule.
        /// </summary>
        /// <value>
        /// The client parameters.
        /// </value>
        public ClientParametersCollection ClientParameters { get; set; }
    }
}