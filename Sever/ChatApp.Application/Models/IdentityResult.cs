using ChatApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Models
{
    public class IdentityResult
    {
        public bool Success { get; private set; } = false;
        public UserInfo User { get; private set; }
        public IdentityResult(bool success,string jwtToken, UserInfo user)
        {
            Success = success;
            User = user;
            JwtToken = jwtToken;
        }   
        public string JwtToken { get; private  set; }

    }
    public class UserInfo
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UrlAvatar { get; set; }
        public Guid Id { get; set; }
    }
}
