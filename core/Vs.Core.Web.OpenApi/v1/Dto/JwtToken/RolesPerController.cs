using System.Collections;
using System.Collections.Generic;

namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    /// <summary>
    /// Contains all the roles available to the controllers capabilities.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{Vs.Core.Web.OpenApi.v1.Dto.JwtToken.RolesCapability}" />
    public class RolesPerController : List<RolesCapability>
    {
    }
}
