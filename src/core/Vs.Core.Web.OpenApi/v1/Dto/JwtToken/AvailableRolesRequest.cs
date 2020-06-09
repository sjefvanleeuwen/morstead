namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    /// <summary>
    /// List available roles request.
    /// </summary>
    public class AvailableRolesRequest
    {
        /// <summary>
        /// Provide full detailing on roles needed per capability (default false)
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is provide role capabilities; otherwise, <c>false</c>.
        /// </value>
        public bool IsProvideRoleCapabilities { get; set; }
    }
}
