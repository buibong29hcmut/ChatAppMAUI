using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Requests.Messages.Commands;
using ChatApp.Share.Wrappers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.API.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ICommandBus _command;
     
        public ChatHub(ICommandBus command)
        {
            _command = command;
        }
        public override async Task OnConnectedAsync()
        {

        }
        public async Task SendMessage(CreateMessageCommand message)
        {

            await _command.Send<Result<Unit>>(message);
            
        }       
    }
}
