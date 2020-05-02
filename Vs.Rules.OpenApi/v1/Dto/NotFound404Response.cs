using System;

namespace Vs.Rules.OpenApi.v1.Dto
{
    /// <summary>
    /// Standard resource not found response (404).
    /// </summary>
    public class NotFound404Response
    {
        /// <summary>
        /// The endoint of the resource that could not be obtained.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        public Uri Endpoint { get; set; }
    }
}
