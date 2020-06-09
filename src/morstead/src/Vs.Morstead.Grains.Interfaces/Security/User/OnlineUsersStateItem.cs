using System;

namespace Vs.Morstead.Grains.Interfaces.Security.User
{
    public class OnlineUsersStateItem
    {
        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public OnlineUserStatusTypes Status { get; set; }
        /// <summary>
        /// Gets or sets the last activity date/time of an online user.
        /// </summary>
        /// <value>
        /// The last activity.
        /// </value>
        public DateTime LastActivity { get; set; }
    }
}
