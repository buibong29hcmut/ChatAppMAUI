using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.Services
{
    public interface ITrackerUserOnline
    {
        Task<bool> IsUserConnected(string userName, string connectionId);
        Task<bool> IsUserDisconnected(string userName, string connectionId);
        Task<bool> GetListUserOnline(string userName,string connectionId);
        Task<List<string>> GetListConnectionByUser(string userName);
    }
}
