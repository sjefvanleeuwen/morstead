using System;

namespace Vs.Core.Web.OpenApi.v1.Dto.ProtocolErrors
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
        /// <summary>
        /// Message that contains the error.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerError500Response"/> class.
        /// </summary>
        public ServerError500Response() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerError500Response" /> class.
        /// </summary>
        /// <param name="ex">The exception to wrap.</param>
        public ServerError500Response(Exception ex)
        {
            Message = ex.Message;
        }
    }
}
