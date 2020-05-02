using System;

namespace Vs.Rules.OpenApi.v1.Dto
{
    /// <summary>
    /// Error response for endpoints that return an invalid swagger json.
    /// </summary>
    public class SwaggerContractErrorResponse
    {
        /// <summary>
        /// The endoint of the resource that contains the invalid swagger json contract.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        public Uri Endpoint { get; set; }

        /// <summary>
        /// Contents of the document that contained the error.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        public string Contents { get; set; }
    }
}
