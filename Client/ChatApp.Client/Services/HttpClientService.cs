
using ChatApp.Client.Contracts;
using ChatApp.Client.Helpers;
using Client.MaUI.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Services
{
    public  class HttpClientService:IHttpClientService
    {
        public HttpClientService()
        {
          
        }
        private  HttpClient OnInitialHttp()
        {
            var devSslHelper = new DevHttpsConnectionHelper(sslPort: 7028);
            var httpClient = devSslHelper.HttpClient;
            var token=  SecureStorage.GetAsync("chattoken").Result;
            if (string.IsNullOrEmpty(token))
            {

                return httpClient;
            }
            httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }
        public  async  Task<T> GetAsync<T>(string uri)
        {
          
                var _httpClient = OnInitialHttp();
                var response =  _httpClient.GetAsync(uri).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    SecureStorage.Remove("chattoken");
                    await Shell.Current.GoToAsync("Login", true);
                    return default;
                }
                var content = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(content)["data"].ToString();
                var result = JsonConvert.DeserializeObject<T>(data);
                return result;

            
       

            
        }
        public  async Task<T> PostAsync<T>(string uri, object val)
        {
            
                var _httpClient = OnInitialHttp();
                HttpContent content = new StringContent(JsonConvert.SerializeObject(val), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(uri, content);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    SecureStorage.Remove("chattoken");
                    await Shell.Current.GoToAsync("Login", true);
                    return default;

                }
                var contentResponse = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(contentResponse)["data"].ToString();
                var result = JsonConvert.DeserializeObject<T>(data);
                return result;
            
          

          }

    }
}

