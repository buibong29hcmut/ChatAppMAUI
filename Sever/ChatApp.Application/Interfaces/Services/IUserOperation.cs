using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.Services
{
    public interface IUserOperation
    {
        Task<List<UserConnection>> GetConnectionByUserName(string userName);
        Task UserConnectedAsync(string userName, string connectionId);
        Task UserDisConnectedAsync(string userName, string connectionId);
        Task<bool> IsUserOnline(string userName);
    }
}
