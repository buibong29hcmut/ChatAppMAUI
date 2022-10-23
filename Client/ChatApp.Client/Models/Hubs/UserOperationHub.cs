using ChatApp.Client.Contracts;
using ChatApp.Client.Helpers;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Hubs
{
    public class UserOperationHub:IUserOperationHub
    {
        private HubConnection _hubConnection;

        public UserOperationHub()
        {
            OnitialHub(); 
        }
        public HubConnection _Hub => _hubConnection;

        public async Task Connect()
        {
           await  _hubConnection.StartAsync();
        }

        public async Task DisConnect()
        {
            await _hubConnection.StopAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _hubConnection.DisposeAsync();
        }

        public void OnitialHub()
        {
            var devSslHelper = new DevHttpsConnectionHelper(sslPort: 7028);
            var token = SecureStorage.GetAsync("chattoken").Result;
            _hubConnection = new HubConnectionBuilder()
           .WithUrl(devSslHelper.DevServerRootUrl + "/userhub", options =>
           {
               options.AccessTokenProvider = () => Task.FromResult(token);
#if ANDROID
               options.HttpMessageHandlerFactory = m => devSslHelper.GetPlatformMessageHandler();
#endif
           }).Build();
        }
    }
}
