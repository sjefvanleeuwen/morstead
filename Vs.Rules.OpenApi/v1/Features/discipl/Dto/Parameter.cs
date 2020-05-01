namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{
    /// <summary>
    /// Abstract implementation for parameters
    /// </summary>
    public abstract class Parameter
    {
        /// <summary>
        /// Short name of the parameter, do not use for unique identification.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Contains the value of the parameter
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }
        /// <summary>
        /// Indicates the type of the parameter for value type casting purposes
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ParameterType Type { get; set; }
    }
}
