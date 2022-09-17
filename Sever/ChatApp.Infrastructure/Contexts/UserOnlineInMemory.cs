using ChatApp.Application.Interfaces.DAL;
using ChatApp.Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Contexts
{
    public  class UserOnlineInMemory: IUserOnlineInMemmory
    {
        private static readonly  ConcurrentDictionary<string, List<UserConnection>> UserOnlines  = new ConcurrentDictionary<string, List<UserConnection>>();
        public void AddUserConnection(string userName, string connectionId)
        {

            if (UserOnlines.ContainsKey(userName))
            {
                UserOnlines[userName].Add(new UserConnection(connectionId));
            }
            List<UserConnection> connections = new List<UserConnection>();
            connections.Add(new UserConnection(connectionId));
            UserOnlines.TryAdd(userName, connections);
        }
        public List<UserConnection> GetListConnection(string userName)
        {
            return UserOnlines[userName];
        }
        public  void RemoveConnection(string userName,string connectionId)
        {
            if (UserOnlines.ContainsKey(userName))
            {
                UserOnlines[userName].Remove(new UserConnection(connectionId));
                if (UserOnlines[userName].Count == 0)
                {
                    UserOnlines.TryRemove(userName, out var item);
                }
            }
        }
        public int CountConnection(string userName)
        {
            return UserOnlines[userName].Count;
        }
        public async  Task<bool> IsUserOnline(string userName)
        {
            return await Task.FromResult(UserOnlines.ContainsKey(userName));
        }
    }
}
