using ChatApp.Application.Interfaces.DAL;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Contexts
{
    public  class UserOperationInMemmory : IUserOperationInMemmory
    {
        private static readonly  ConcurrentDictionary<string, List<UserConnection>> UserOnlines  = new ConcurrentDictionary<string, List<UserConnection>>();
        public void AddUserConnection(string userId, string connectionId)
        {

            if (UserOnlines.ContainsKey(userId))
            {
                UserOnlines[userId].Add(new UserConnection(connectionId));
            }
            List<UserConnection> connections = new List<UserConnection>();
            connections.Add(new UserConnection(connectionId));
            UserOnlines.TryAdd(userId, connections);
        }
        public List<UserConnection> GetListConnection(string userName)
        {   if (!UserOnlines.ContainsKey(userName))
                return new();
            return UserOnlines[userName];
        }
        public  void RemoveConnection(string userId, string connectionId)
        {
            if (UserOnlines.ContainsKey(userId))
            {
                UserOnlines[userId].Remove(new UserConnection(connectionId));
                if (UserOnlines[userId].Count == 0)
                {
                    UserOnlines.TryRemove(userId, out var item);
                }
            }
        }
        public int CountConnection(string userId)
        {   if (!UserOnlines.ContainsKey(userId))
                return 0;
            return UserOnlines[userId].Count;
        }
        public async  Task<bool> IsUserOnline(string userId)
        {
            return await Task.FromResult(UserOnlines.ContainsKey(userId));
        }
    }
}
