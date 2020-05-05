using System.Collections.Generic;

namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    /// <summary>
    /// Signs a token request using a private key
    /// </summary>
    public class CreateSignedTokenRequest
    {
        /// <summary>
        /// private key for signing the token.
        /// </summary>
        /// <value>
        /// The private key.
        /// </value>
        public string PrivateKey { get; set; }
        /// <summary>
        /// Time to Live (TTL) in seconds before the token expires.
        /// </summary>
        /// <value>
        /// The TTL.
        /// </value>
        public int TTL { get; set; }
        /// <summary>
        /// Gets or sets the issuer of the token.
        /// </summary>
        /// <value>
        /// The issuer.
        /// </value>
        public string Issuer { get; set; }
        /// <summary>
        /// Gets or sets the authority of the token.
        /// </summary>
        /// <value>
        /// The authority.
        /// </value>
        public string Authority { get; set; }
        /// <summary>
        /// Gets or sets the subject of the token.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }
        /// <summary>
        /// Roles for the claim
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<Role> Roles {get;set;}
    }
}
