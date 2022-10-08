using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Requests.Messages.Commands;
using ChatApp.Share.Wrappers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.API.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ICommandBus _command;
        private readonly IUserOperation _operation;
     
        public ChatHub(ICommandBus command, IUserOperation operation)
        {
            _command = command;
            _operation = operation; 
        }

        public async Task SendMessageToConversation(string ToUser,CreateMessageCommand message)
        {
            var userConnections=   await  _operation.GetConnectionByUserName(ToUser);
            var userConnectionIds = userConnections.Select(p => p.connectionId);
            await _command.Send<Result<Unit>>(message);
            await Clients.Clients(userConnectionIds)
                 .SendAsync("ReceiveMessage", message.FromUserId, message.Content);

        }       
    }
}
