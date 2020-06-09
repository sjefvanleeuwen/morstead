using System.Collections.Generic;

namespace Vs.Morstead.Grains.Interfaces.Security.User
{
    public class OnlineUsersState
    {
        public Dictionary<string, OnlineUsersStateItem> OnlineUsers { get; set; }
    }
}
