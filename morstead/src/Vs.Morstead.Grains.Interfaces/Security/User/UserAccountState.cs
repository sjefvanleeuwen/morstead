using System.Globalization;

namespace Vs.Morstead.Grains.Interfaces.Security.User
{
    public class UserAccountState
    {
        public CultureInfo Locale { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
    }
}
