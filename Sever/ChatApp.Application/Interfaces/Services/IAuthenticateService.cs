using ChatApp.Application.Models;
using ChatApp.Application.Requests.Users;
using ChatApp.Application.Requests.Users.Commands;
using ChatApp.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.Services
{
    public interface IAuthenticateService
    {
        Task<Result<UserIdentity>> LoginAsync(UserForLoginOrRegister userInfo);
        Task<Result<UserIdentity>> RegisterAsync(UserForRegisterCommand newUser);
    }
}
