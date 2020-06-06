using System.Globalization;

namespace Vs.Rules.Grains.Interfaces.User
{
    public class UserAccountState
    {
        public CultureInfo Locale { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
    }
}
