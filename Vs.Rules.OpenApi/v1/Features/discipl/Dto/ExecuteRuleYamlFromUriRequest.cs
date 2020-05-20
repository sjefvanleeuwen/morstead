using System;

namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
    /// <summary>
    /// Execute a rule by requesting it from a url (endpoint) and evaluating it against a collection of client parameters.
    /// </summary>
    /// <seealso cref="Vs.Rules.OpenApi.v1.Features.discipl.Dto.ExecuteRuleYamlRequestBase" />
    public class ExecuteRuleYamlFromUriRequest : ExecuteRuleYamlRequestBase
    {
        public Uri Endpoint { get; set; }       
    }
}