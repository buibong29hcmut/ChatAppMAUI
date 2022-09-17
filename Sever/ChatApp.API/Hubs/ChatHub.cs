using ChatApp.Application.Cores.Commands;
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
        public async Task SendMessage()
        {

        }
       
    }
}
