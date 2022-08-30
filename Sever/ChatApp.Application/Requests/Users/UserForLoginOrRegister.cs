using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Users
{
    public class UserForLoginOrRegister
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public UserForLoginOrRegister(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }   
    }
}
