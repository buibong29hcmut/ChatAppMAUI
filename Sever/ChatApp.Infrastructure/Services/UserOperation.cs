using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Services
{
    public class UserOperation : IUserOperation
    {
        private readonly IUserOperationInMemmory _memoryStoreStatus;
        public UserOperation(IUserOperationInMemmory memoryStoreStatus
            )
        {
            _memoryStoreStatus = memoryStoreStatus;
        }
        public async Task<List<UserConnection>> GetConnectionByUserName(string userName)
        {
            return await Task.FromResult(_memoryStoreStatus.GetListConnection(userName));
        }

        public async Task<bool> IsUserOnline(string userId)
        {
            int countConnectionId = _memoryStoreStatus.CountConnection(userId);
            return await Task.FromResult(countConnectionId > 0);
        }

        public async Task UserConnectedAsync(string userId, string connectionId)
        {
            _memoryStoreStatus.AddUserConnection(userId, connectionId);
            Console.WriteLine($"{userId} has connectionId {connectionId}");
            await Task.CompletedTask;
        }

        public async Task UserDisConnectedAsync(string userName, string connectionId)
        {
            _memoryStoreStatus.RemoveConnection(userName, connectionId);
            Console.WriteLine($"{userName} with connectionId {connectionId} disconnected");
            await Task.CompletedTask;
        }
    }
}
