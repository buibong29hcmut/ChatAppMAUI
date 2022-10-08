using ChatApp.API.Models;
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

        public async Task SendMessageToConversation(MessageForSendConversation messageDto)
        {
            var messagecmd = new CreateMessageCommand()
            {
                FromUserId = messageDto.User.UserId,
                Content = messageDto.Content,
                ConversationId = messageDto.ConversationId,
                SendTime = messageDto.SendTime,
            };
            var userConnections=   await  _operation.GetConnectionByUserName(messageDto.User.UserName);
            var userConnectionIds = userConnections.Select(p => p.connectionId);
            await _command.Send<Result<Unit>>(messagecmd);
            await Clients.Clients(userConnectionIds)
                 .SendAsync("ReceiveMessage",messageDto.User.UserName,messageDto.User.Name, messagecmd.Content);

        }       
    }
}
