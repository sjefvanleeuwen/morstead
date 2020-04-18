using Newtonsoft.Json;

namespace Vs.Rules.OpenApi.v2.Dto
{
    public class ParseResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("isError")]
        public bool IsError { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
