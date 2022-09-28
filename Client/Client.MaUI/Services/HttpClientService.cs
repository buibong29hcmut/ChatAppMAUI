﻿
using Client.MaUI.Contracts;
using Client.MaUI.Helpers;
using Client.MaUI.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.MaUI.Services
{
    public  class HttpClientService:IHttpClientService
    {
        public HttpClientService()
        {
          
        }
        private  HttpClient OnInitialHttp()
        {
            var devSslHelper = new DevHttpsConnectionHelper(sslPort: 7068);
            var httpClient = devSslHelper.HttpClient;
            var token=  SecureStorage.GetAsync("chattoken").Result;
            if (string.IsNullOrEmpty(token))
            {

                return default;
            }
            httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }
        public  async  Task<T> GetAsync<T>(string uri)
        {
            var _httpClient= OnInitialHttp();
            var response = await _httpClient.GetAsync(uri);
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
            var _httpClient =  OnInitialHttp();
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

