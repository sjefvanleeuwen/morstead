using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Vs.VoorzieningenEnRegelingen.Site.Shared;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    [Authorize]
    public class UserProfile
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }


    }

}
