using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
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
            return StatusCode(200, new CreateSignedTokenResponse() { Token = tokenHandler.WriteToken(token) });
        }

        /// <summary>
        /// Lists all available roles from controllers and their capabilities.
        /// </summary>
        /// <param name="request">The request containing available roles request</param>
        /// <returns>An unsigned token</returns>
        /// <response code="200">The token was created successfully</response>
        /// <response code="404">No roles found for any controller.</response>
        /// <response code="500">Server error</response>
        [HttpPost("available-roles")]
        [ProducesResponseType(typeof(AvailableRolesResponse), 200)]
        [ProducesResponseType(typeof(NotFound404Response), 404)]
        [ProducesResponseType(typeof(ServerError500Response), 500)]
        public async Task<IActionResult> AvailableRoles(AvailableRolesRequest request)
        {
            var RolesControllerDictionary = new Dictionary<string, List<string>>();
            const bool allInherited = true;
            const string CONTROLLER = "Controller";

            var myAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            // get List of all Controllers with [Authorize] attribute
            var controllerList = from type in myAssembly.GetTypes()
                                 where type.Name.Contains(CONTROLLER)
                                 where !type.IsAbstract
                                 let attribs = type.GetCustomAttributes(allInherited)
                                 where attribs.Any(x => x.GetType().Equals(typeof(AuthorizeAttribute)))
                                 select type;
            // Loop over all controllers
            foreach (var controller in controllerList)
            {   // Find first instance of [Authorize] attribute
                var attrib = controller.GetCustomAttributes(allInherited).First(x => x.GetType().Equals(typeof(AuthorizeAttribute))) as AuthorizeAttribute;
                foreach (var role in attrib.Roles.Split(',').AsEnumerable())
                {   // If there are Roles associated with [Authorize] iterate over them
                    if (!RolesControllerDictionary.ContainsKey(role))
                    { RolesControllerDictionary[role] = new List<string>(); }
                    // Add controller to List of controllers associated with role (removing "controller" from name)
                    RolesControllerDictionary[role].Add(controller.Name.Replace(CONTROLLER, ""));
                }
            }
            return StatusCode(200,RolesControllerDictionary);

            //var response = new AvailableRolesResponse();

        }
    }
}

