using ChatApp.Client.Contracts;
using ChatApp.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Services
{
    public class AuthenticateClientService : IAuthenticateClientService
    {
        private readonly IHttpClientService _http;
        public AuthenticateClientService(IHttpClientService http)
        {
            _http = http;
        }

        public async Task Login(string userName, string Password)
        {
            var response = await _http.PostAsync<AuthenticateModel>("api/v1/auth", new { userName = userName, password = Password });
            await SecureStorage.SetAsync("chattoken", response.JwtToken);
            await SecureStorage.SetAsync("profile",response.Info.Id.ToString());
        }
        public async Task LoginGoogle()
        {
            await Task.CompletedTask;
        }
        public async Task LoginFacebook()
        {
            await Task.CompletedTask;
        }
    }
    public class AuthenticateResult
    {
        public string JwtToken { get; set; }
    }
}
