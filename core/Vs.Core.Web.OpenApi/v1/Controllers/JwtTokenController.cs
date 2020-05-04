using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NSwag.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vs.Core.Web.OpenApi.v1.Dto.JwtToken;
using Vs.Core.Web.OpenApi.v1.Dto.ProtocolErrors;

namespace Vs.Core.Web.OpenApi.v1.Controllers
{
    [ApiVersion("1.0-core")]
    [Route("api/v{version:apiVersion}/core")]
    [OpenApiTag("JWT Token Controller", Description = "JWT Token Controller for API clients")]
    [ApiController]
    public class JwtTokenController : VsControllerBase
    {
        /// <summary>
        /// Creates a signed JWT Token. !NOTE do not distribute your private key to this endpoint unless you own this API server and the signature.
        /// </summary>
        /// <param name="request">The request containing the endpoint to the API swagger json contract to generate the code from</param>
        /// <returns>An unsigned token</returns>
        /// <response code="200">The token was created successfully</response>
        /// <response code="404">No valid roles provided.</response>
        /// <response code="500">Server error</response>
        [HttpPost("create-token")]
        [ProducesResponseType(typeof(CreateSignedTokenResponse), 200)]
        [ProducesResponseType(typeof(NotFound404Response), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> CreateSignedToken(CreateSignedTokenRequest request)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            foreach (var role in request.Roles)
            { 
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name)); 
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(issuer: request.Issuer,
                audience: request.Authority,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(request.TTL),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.Default.GetBytes(request.PrivateKey)),
                        SecurityAlgorithms.HmacSha256Signature));
            return StatusCode(200, new CreateSignedTokenResponse());
        }

        /// <summary>
        /// Lists all available roles
        /// </summary>
        /// <param name="request">The request containing available roles request</param>
        /// <returns>An unsigned token</returns>
        /// <response code="200">The token was created successfully</response>
        /// <response code="404">No valid roles provided.</response>
        /// <response code="500">Server error</response>
        [HttpPost("available-roles")]
        [ProducesResponseType(typeof(AvailableRolesResponse), 200)]
        [ProducesResponseType(typeof(NotFound404Response), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> AvailableRoles(AvailableRolesRequest request)
        {
            throw new NotImplementedException();
        }

    }
}
