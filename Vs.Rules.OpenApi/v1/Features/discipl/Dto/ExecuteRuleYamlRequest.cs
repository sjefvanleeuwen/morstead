using System;

namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
    /// <summary>
    /// Abstract implementation for rule execution.
    /// </summary>
    public abstract class ExecuteRuleYamlRequest
    {
        /// <summary>
        /// Gets or sets the client parameters used to evaluate the rule.
        /// </summary>
        /// <value>
        /// The client parameters.
        /// </value>
        public ClientParametersCollection ClientParameters { get; set; }
    }

    /// <summary>
    /// Execute a rule by requesting it from a url (endpoint) and evaluating it against a collection of client parameters.
    /// </summary>
    /// <seealso cref="Vs.Rules.OpenApi.v1.Features.discipl.Dto.ExecuteRuleYamlRequest" />
    public class ExecuteRuleYamlFromUriRequest : ExecuteRuleYamlRequest
    {
        public Uri Endpoint { get; set; }       
    }
}