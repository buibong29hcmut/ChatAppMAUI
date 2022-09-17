using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Users.Commands
{
    public class UserForLoginOrRegister
    {
        public string UserName { get; protected set; }
        public string Password { get; protected set; }
        public UserForLoginOrRegister(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
