namespace Vs.Core.Web.OpenApi.v1.Dto.CodeGenerators
{
    /// <summary>
    /// Base response message for code generation to consume the API.
    /// </summary>
    public abstract class GenerateCodeClientResponse
    {
        /// <summary>
        /// Generated client code.
        /// </summary>
        /// <value>
        /// The client code.
        /// </value>
        public string ClientCode { get; set; }
    }

    public abstract class GenerateCodeServerResponse
    {
        /// <summary>
        /// Generated client code.
        /// </summary>
        /// <value>
        /// The client code.
        /// </value>
        public string ServerCode { get; set; }
    }
}
