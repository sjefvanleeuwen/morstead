using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
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
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var typ = assembly.GetTypes();
                types.AddRange((from Type type in typ where type.IsSubclassOf(typeof(ControllerBase)) select type).ToArray());
            }

            var authMethods = types.SelectMany(x => x.GetMethods())
            .Where(y => y.GetCustomAttributes().OfType<AuthorizeAttribute>().Any())
            .ToDictionary(z => z.Name);

            var availableRoles = new AvailableRolesResponse();
            availableRoles.RolesControllers = new RolesControllers
            {
                Controllers = new List<RolesController>(),
                DistinctRoles = new List<Role>()
            };

            Type currentController = typeof(bool);
            RolesController currentRolesController = null;
            foreach (var authMethod in authMethods)
            {
                // controllers are discovered in ordered order.
                if (authMethod.Value.DeclaringType != currentController)
                {
                    currentRolesController = new RolesController
                    {
                        RolesPerCapability = new List<RolesCapability>(),
                        DistinctRoles = new List<Role>(),
                        Name = authMethod.Value.DeclaringType.Name
                    };
                    availableRoles.RolesControllers.Controllers.Add(currentRolesController);
                }

                dynamic s = authMethod.Value.GetCustomAttribute(typeof(AuthorizeAttribute));
                //todo get method name from decoration HTTPPOST/HTTPGET. Currently just refelcts the c# method.
                var rolesPerCapability = new RolesCapability
                {
                    Name = authMethod.Value.Name,
                    //rolesPerCapability.DistinctRoles = new Roles();
                    DistinctRoles = new List<Role>()
                };

                foreach (var role in ((string)s.Roles).Split(',').Select(sValue => sValue.Trim()).ToArray())
                {
                    rolesPerCapability.DistinctRoles.Add(new Role() { Name = role });
                    // Add the capabilities roles distinctly to Controller level
                    if (!currentRolesController.DistinctRoles.Any(p => p.Name == role))
                        currentRolesController.DistinctRoles.Add(new Role() { Name = role });
                    if (!availableRoles.RolesControllers.DistinctRoles.Any(p => p.Name == role))
                        // Add the capabilities roles distinctly to API level
                        availableRoles.RolesControllers.DistinctRoles.Add(new Role() { Name = role });
                }
                currentRolesController.RolesPerCapability.Add(rolesPerCapability);
            }

            return StatusCode(200,availableRoles);
        }
    }
}

