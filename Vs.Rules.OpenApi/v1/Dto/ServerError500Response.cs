using System;

namespace Vs.Rules.OpenApi.v1.Dto
{
    /// <summary>
    /// Standard Server Error 500 Response
    /// </summary>
    public class ServerError500Response
    {
        /// <summary>
        /// The endoint of the resource that could caused the error.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        public Uri Endpoint { get; set; }
    }
}
