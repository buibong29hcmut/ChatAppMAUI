using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.Services
{
    public interface IUserOperation
    {
        Task UserConnectedAsync(string userName, string connectionId);
        Task UserDisConnectedAsync(string userName, string connectionId);
        Task<bool> IsUserOnline(string userName);
    }
}
