using System.Collections.Generic;

namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    /// <summary>
    /// Contains all the roles that are used by this controller.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{Vs.Core.Web.OpenApi.v1.Dto.JwtToken.RolesCapability}" />
    public class RolesController
    {
        public List<RolesCapability> RolesPerCapability { get; set; }
        /// <summary>
        /// Name of the Controller
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Distinct Roles used by this API Controller
        /// </summary>
        /// <value>
        /// The distinct roles.
        /// </value>
        public List<Role> DistinctRoles { get; set; }
    }
}
