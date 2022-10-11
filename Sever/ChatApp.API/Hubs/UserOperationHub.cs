using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApp.API.Hubs
{
    [Authorize]   
    public class UserOperationHub:Hub
    {
        private readonly IUserOperation _operation;
        public UserOperationHub(IUserOperation operation)
        {
            _operation = operation;
        }

         public override async Task OnConnectedAsync()
        {
            string userId = Context.GetHttpContext().User.Claims.FirstOrDefault(p => p.Equals(ClaimTypes.NameIdentifier)).Value;
            var offline = await _operation.IsUserOnline(userId);
            await _operation.UserConnectedAsync(userId, Context.ConnectionId);
            if (offline==true)
            {
                await Clients.All.SendAsync("UserOnline", userId);
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string userName = Context.GetHttpContext().User.Claims.FirstOrDefault(p => p.Equals(ClaimTypes.Name)).Value;
            await _operation.UserDisConnectedAsync(userName, Context.ConnectionId);
            var userOnline = await _operation.IsUserOnline(userName);

            if (userOnline==false)
            {
                await Clients.All.SendAsync("UserOffline", userName);

            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
