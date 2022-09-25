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
            string JwtToken = SecureStorage.GetAsync("chattoken").Result;

            _httpClient.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", JwtToken);
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
            string JwtToken = SecureStorage.GetAsync("chattoken").Result;

            _httpClient.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", JwtToken);
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
