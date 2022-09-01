using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Users
{
    public class LoginOrRegisterUserCommand
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }    
        public LoginOrRegisterUserCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }   
    }
}
