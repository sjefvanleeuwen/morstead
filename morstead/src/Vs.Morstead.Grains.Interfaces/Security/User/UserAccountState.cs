using System;
using System.Globalization;

namespace Vs.Morstead.Grains.Interfaces.User
{
    public class UserAccountState
    {
        public CultureInfo Locale { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        // External profile picture URL, such as: https://www.gravatar.com/avatar/HASH
        public Uri Picture { get; set; }
    }
}
