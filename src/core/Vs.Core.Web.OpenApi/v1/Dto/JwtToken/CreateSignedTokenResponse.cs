namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    public class CreateSignedTokenResponse
    {
        /// <summary>
        /// The created token in base64.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }
    }
}
