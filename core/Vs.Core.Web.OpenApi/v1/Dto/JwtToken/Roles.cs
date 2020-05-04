using System.Collections;
using System.Collections.Generic;

namespace Vs.Core.Web.OpenApi.v1.Dto.JwtToken
{
    public class Roles : IEnumerable<Role>
    {
        List<Role> roles = new List<Role>();

        public Role this[int index]
        {
            get { return roles[index]; }
            set { roles.Insert(index, value); }
        }

        public IEnumerator<Role> GetEnumerator()
        {
            return roles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
