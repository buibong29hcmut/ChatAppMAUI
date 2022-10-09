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
                };
                var userConnections = await _operation.GetConnectionByUserName(UserId);
                var userConnectionIds = userConnections.Select(p => p.connectionId);
                await _command.Send<Result<Unit>>(messagecmd);
                await Clients.Clients(userConnectionIds)
                     .SendAsync("ReceiveMessage", new MessageRecieve
                     {
                         FromUserId = new Guid(UserId),
                         Content = messageDto.Content,
                         Id = messageDto.ConversationId,
                         SendTime = messageDto.SendTime,
                         IsThisUser = false
                     });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }       
    }
}
