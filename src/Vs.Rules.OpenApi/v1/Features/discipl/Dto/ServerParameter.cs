namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{

    /// <summary>
    /// A Server Parameter
    /// </summary>
    public class ServerParameter : Parameter
    {

        /// <summary>
        /// Indicates the type of the parameter for value type casting purposes
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ParameterType Type { get; set; }
    }
}
