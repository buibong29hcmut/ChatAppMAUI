using ChatApp.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Contracts
{
    public interface IAuthenticateClientService
    {
        Task Login(string userName, string Password);
        Task RegisterAsync(RegisterUserModel User);
    }
}
