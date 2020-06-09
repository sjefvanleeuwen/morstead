using System.Collections.Generic;

namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    /// <summary>
    /// Contains the roles used by a capability.
    /// </summary>
    public class RolesCapability
    {
        /// <summary>
        /// Name of the capability (method)
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Distinct Roles available to the  capbilitity
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<Role> DistinctRoles { get; set; }
    }
}