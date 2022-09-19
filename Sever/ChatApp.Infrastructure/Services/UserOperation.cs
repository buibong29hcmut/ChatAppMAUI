using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Services
{
    public class UserOperation : IUserOperation
    {
        private readonly IUserOperationInMemmory _memoryStoreStatus;
        public UserOperation(IUserOperationInMemmory memoryStoreStatus)
        {
            _memoryStoreStatus = memoryStoreStatus;
        }

        public async Task<bool> IsUserOnline(string userName)
        {
            return await Task.FromResult(_memoryStoreStatus.CountConnection(userName) != 0);
        }

        public async Task UserConnectedAsync(string userName, string connectionId)
        {
            _memoryStoreStatus.AddUserConnection(userName, connectionId);
            await Task.CompletedTask;
        }

        public async Task UserDisConnectedAsync(string userName, string connectionId)
        {
            _memoryStoreStatus.RemoveConnection(userName, connectionId);
            await Task.CompletedTask;
        }
    }
}
