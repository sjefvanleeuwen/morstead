namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto
{

    /// <summary>
    /// Contains a question asked by the server that the client should answer.
    /// </summary>
    public class QuestionParameter : Parameter
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
