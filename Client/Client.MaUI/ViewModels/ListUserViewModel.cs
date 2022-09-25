using Client.MaUI.DataStructures;
using Client.MaUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Share;
using ChatApp.Share.Wrappers;
using Microsoft.Toolkit.Mvvm.Input;

namespace Client.MaUI.ViewModels
{
    public partial class ListUserViewModel:BaseViewModel
    {
        private readonly HttpClient _httpClient;
        public RangeObservableCollection<UserModel> Users { get; private set; } = new();
        private int PageNumber = 0;
        bool HasNextPage;
        public ListUserViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            GetListUser();
            
        }
        public  void GetListUser()
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOlsiNWIxYTk5ZjktZGZhOC00MTE3LWJhMjYtOTMwODQ0NjlkMzVlIiwiYnVpYm9uZzI5MTIiXSwibmJmIjoxNjYzNzMyNjY1LCJleHAiOjE2NjQzMzc0NjMsImlhdCI6MTY2MzczMjY2NSwiaXNzIjoiSldUQXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJKV1RTZXJ2aWNlUG9zdG1hbkNsaWVudCJ9.WcHICsqq0OQ1PO4L3Xq5GUMrZGQJlTbo7he_6host5g";
            _httpClient.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", token);
            var response =  _httpClient.GetAsync("api/v1/user?pageSize=10&pageNumber=1").Result;
            var content =  response.Content.ReadAsStringAsync().Result;
            var data = JObject.Parse(content)["data"].ToString();
            var firstUserPage = JsonConvert.DeserializeObject<PageList<UserModel>>(data);
            Users.AddRange(firstUserPage.Items);
            HasNextPage = firstUserPage.HasNextPage;
        }
        [ICommand]
        public async void LoadMoreUserAsync()
        {
            if (IsBusy==true|| HasNextPage==false)
                return;
            IsBusy = true;
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOlsiNWIxYTk5ZjktZGZhOC00MTE3LWJhMjYtOTMwODQ0NjlkMzVlIiwiYnVpYm9uZzI5MTIiXSwibmJmIjoxNjYzNzMyNjY1LCJleHAiOjE2NjQzMzc0NjMsImlhdCI6MTY2MzczMjY2NSwiaXNzIjoiSldUQXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJKV1RTZXJ2aWNlUG9zdG1hbkNsaWVudCJ9.WcHICsqq0OQ1PO4L3Xq5GUMrZGQJlTbo7he_6host5g";
            _httpClient.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"api/v1/user?pageSize=10&pageNumber={PageNumber+1}");
            var content = await response.Content.ReadAsStringAsync();

            var data = JObject.Parse(content)["data"].ToString();
            var listUser = JsonConvert.DeserializeObject<PageList<UserModel>>(data);
            Users.AddRange(listUser.Items);
            HasNextPage = listUser.HasNextPage;
            PageNumber += 1;
            IsBusy = false;
        }
      

    }
}
