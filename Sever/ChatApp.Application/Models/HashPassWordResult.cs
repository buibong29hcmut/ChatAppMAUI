using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Models
{
    public class HashPassWordResult
    {
        public string Salt { get; private set; }
        public string PasswordHash { get; private set; }
        public HashPassWordResult(string salt, string passwordHash)
        {
            Salt = salt;
            PasswordHash = passwordHash;
        }   
    }
}
