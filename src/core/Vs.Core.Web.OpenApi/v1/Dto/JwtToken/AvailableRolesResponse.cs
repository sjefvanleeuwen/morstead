namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    /// <summary>
    /// lists all available roles for the API.
    /// </summary>
    public class AvailableRolesResponse
    {
        /// <summary>
        /// Controllers containing authorization roles.
        /// </summary>
        /// <value>
        /// The roles controllers.
        /// </value>
        public RolesControllers RolesControllers { get; set; }
    }
}
