using ChatApp.API.Models;
using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Requests.Messages.Commands;
using ChatApp.Share.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApp.API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub:Hub
    {
        private readonly ICommandBus _command;
        private readonly IUserOperation _operation;
     
        public ChatHub(ICommandBus command, IUserOperation operation)
        {
            _command = command;
            _operation = operation; 
        }
        public override async Task OnConnectedAsync()
        {
            string userId = Context.GetHttpContext().User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var offline = await _operation.IsUserOnline(userId);
            await _operation.UserConnectedAsync(userId, Context.ConnectionId);
            if (offline == true)
            {
                await Clients.All.SendAsync("UserOnline", userId);
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string userName = Context.GetHttpContext().User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.Name)).Value;
            await _operation.UserDisConnectedAsync(userName, Context.ConnectionId);
            var userOnline = await _operation.IsUserOnline(userName);

            if (userOnline == false)
            {
                await Clients.All.SendAsync("UserOffline", userName);

            }
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessageToConversation(MessageForSendConversation messageDto)
        {
            try
            {
                string UserId = Context.GetHttpContext().
                    User.Claims.
                    FirstOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier)).Value;

                var messagecmd = new CreateMessageCommand()
                {
                    FromUserId = new Guid(UserId),
                    Content = messageDto.Content,
                    ConversationId = messageDto.ConversationId,
                    SendTime = messageDto.SendTime,
                    ToUserId= messageDto.ToUserId,
                };
                var userConnections = await _operation.GetConnectionByUserName(UserId);
                var userConnectionIds = userConnections.Select(p => p.connectionId).ToList();
                userConnectionIds.Remove(Context.ConnectionId);
                await _command.Send<Result<Unit>>(messagecmd);
                await Clients.Clients(userConnectionIds)
                     .SendAsync("ReceiveMessage", new MessageRecieve
                     {
                         FromUserId = new Guid(UserId),
                         Content = messageDto.Content,
                         Id = messageDto.ConversationId,
                         SendTime = messageDto.SendTime,
                         IsThisUser = true
                     });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }       
    }
}
