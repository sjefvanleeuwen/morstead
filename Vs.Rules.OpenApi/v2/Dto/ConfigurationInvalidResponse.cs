namespace Vs.Rules.OpenApi.v2.Dto
{
    public class ConfigurationInvalidResponse
    {
        /// <summary>
        /// Gets or sets the content resource.
        /// </summary>
        /// <value>
        /// The content resource.
        /// </value>
        public ResourceResponse ContentResource { get; set; }
        /// <summary>
        /// Gets or sets the rule resource.
        /// </summary>
        /// <value>
        /// The rule resource.
        /// </value>
        public ResourceResponse RuleResource { get; set; }
    }
}