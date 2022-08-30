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
        public User User { get; private set; }
        public IdentityResult(bool success,string JwtToken, User user)
        {
            Success = success;
            User = user;
        }   
        public string JwtToken { get; private  set; }

    }
}
