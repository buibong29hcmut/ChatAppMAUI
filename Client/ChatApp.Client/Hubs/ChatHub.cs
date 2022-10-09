
using ChatApp.Client.Contracts;
using ChatApp.Client.Helpers;
using ChatApp.Client.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Hubs
{
    public class ChatHub:IChatHub
    {
        private  HubConnection _hubConnection;
        private  List<Action<string,string,string>> _handlers;
        public ChatHub()
        {
            OnitialHub();
        }
        private void OnitialHub()
        {
            var devSslHelper = new DevHttpsConnectionHelper(sslPort: 7028);
            var token = SecureStorage.GetAsync("chattoken").Result;
            _hubConnection = new HubConnectionBuilder()
           .WithUrl(devSslHelper.DevServerRootUrl + "/chathub", options =>
           {
               options.AccessTokenProvider = () => Task.FromResult(token);
#if ANDROID
               options.HttpMessageHandlerFactory = m => devSslHelper.GetPlatformMessageHandler();
#endif
           }).Build();
            _hubConnection.On<string, string, string>("ReceiveMessage", OnRecieveMessage);
            _handlers= new();

       
        }
        public async Task Connect()
        {
           await _hubConnection.StartAsync();
        }
        public async Task DisConnect()
        {
            await _hubConnection.StopAsync();
          
        }
        public async Task SendMessageToConversation(MessageForSendConversation message)
        {
            await _hubConnection.InvokeAsync("SendMessageToConversation", message);

        }
        public void OnRecieveMessage(string userName,string name, string content)
        {
            foreach(var handler in _handlers)
            {
                handler(userName,name, content);
            }
        }
        public ValueTask DisposeAsync()
        {
           return  _hubConnection.DisposeAsync();
        }
    }
}
