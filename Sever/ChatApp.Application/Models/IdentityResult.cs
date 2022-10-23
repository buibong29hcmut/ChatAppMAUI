using ChatApp.Domain.Entities;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Models
{
    public class IdentityResult:Result<UserIdentity>
    {
        
    }
    public class UserInfo
    {
        public Guid Id { get; set; }
    }
    public class UserIdentity
    {
        public string JwtToken { get; set; }
        public UserInfo Info { get; set; }
    }
}
