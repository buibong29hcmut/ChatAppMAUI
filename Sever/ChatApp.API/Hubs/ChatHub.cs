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
            string userId = Context.GetHttpContext().User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            await _operation.UserDisConnectedAsync(userId, Context.ConnectionId);
            var userOnline = await _operation.IsUserOnline(userId);

            if (userOnline == false)
            {
                await Clients.All.SendAsync("UserOffline", userId);

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
                var userOtherConnections = await _operation.GetConnectionByUserName(messageDto.ToUserId.ToString());
                userConnections.AddRange(userOtherConnections);
             
                var userConnectionsId= userConnections.Select(p => p.connectionId).ToList();
                userConnectionsId.Remove(this.Context.ConnectionId);
                var result = await _command.Send<Result<Unit>>(messagecmd, default);
                await Clients.Clients(userConnectionsId)
                     .SendAsync("ReceiveMessage", new MessageRecieve
                     {
                         FromUserId = new Guid(UserId),
                         Content = messageDto.Content,
                         Id = messageDto.ConversationId,
                         SendTime = messageDto.SendTime,
                     });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }       
    }
}
