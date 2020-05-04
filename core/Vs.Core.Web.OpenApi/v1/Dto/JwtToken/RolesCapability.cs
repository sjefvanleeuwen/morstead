namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    /// <summary>
    /// Contains the roles available to a capability.
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
        /// Roles available to the controller.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public Roles Roles { get; set; }
    }
}