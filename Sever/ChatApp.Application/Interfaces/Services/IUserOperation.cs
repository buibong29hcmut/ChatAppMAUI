using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.Services
{
    public interface IUserOperation
    {
        Task<bool> UserConnectedAsync(string userName, string connectionId);
        Task<bool> UserDisConnectedAsync(string userName, string connectionId);
        bool IsUserOnline(string userName);
    }
}
