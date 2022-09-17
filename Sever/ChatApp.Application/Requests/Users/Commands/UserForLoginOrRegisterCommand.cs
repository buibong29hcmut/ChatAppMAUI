using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Models;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Requests.Users.Commands
{
    public class UserForLoginOrRegisterCommand : UserForLoginOrRegister,ICommand<Result<IdentityResult>>
    {
      
        public UserForLoginOrRegisterCommand(string userName, string password):base(userName, password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
