using System.Collections.Generic;

namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    public class RolesControllers
    {
        /// <summary>
        /// List of controllers that contain authorization attributes.
        /// </summary>
        /// <value>
        /// The controllers.
        /// </value>
        public List<RolesController> Controllers {get;set;}

        /// <summary>
        /// Distinct Roles used by the API.
        /// </summary>
        /// <value>
        /// The distinct roles.
        /// </value>
        public List<Role> DistinctRoles { get; set; }
    }
}
