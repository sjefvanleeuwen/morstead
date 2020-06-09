using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class UserProfile
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Occupation { get; set; }
        public long Followers { get; set; }
        public long Colleagues { get; set; }
        public long Following { get; set; }
        public string Education { get; set; }
        public string Municipality { get; set; }
        public IEnumerable<string> Skills { get; set; }
        public string Info { get; set; }

        public UserProfile()
        {

        }
    }
}
