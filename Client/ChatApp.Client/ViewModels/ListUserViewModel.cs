
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
using ChatApp.Client.DataStructures;
using ChatApp.Client.Models;
using ChatApp.Client.Services;
using ChatApp.Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChatApp.Client.ViewModels
{
    public partial class ListUserViewModel:BaseViewModel
    {
        private readonly HttpClientService _httpClient;
        [ObservableProperty]
        private RangeObservableCollection<UserModel> users;
        [ObservableProperty]
        private UserProfileModel user;
        private int PageNumber = 1;
        bool HasNextPage;
        public ListUserViewModel()
        {
            _httpClient= new HttpClientService();
            Users = new();
            GetListUser();
            Task.Run(async () => await GetProfileUser());
            
        }
        public  void GetListUser()
        {           
            var data =  _httpClient.GetAsync<PageList<UserModel>>($"api/v1/user?pageSize=10&pageNumber={PageNumber}").Result;     
            Users.AddRange(data.Items);
            HasNextPage = data.HasNextPage;
        }
        public async Task GetProfileUser()
        {
            var userId = await SecureStorage.GetAsync("profile");
            User = await _httpClient.GetAsync<UserProfileModel>($"api/v1/user/{userId}");

        }
        [ICommand]
        public async void LoadMoreUserAsync()
        {
            if (IsBusy==true|| HasNextPage==false)
                return;
            IsBusy = true;
            var listUser = await _httpClient.GetAsync<PageList<UserModel>>($"api/v1/user?pageSize=10&pageNumber={PageNumber+1}");     
            Users.AddRange(listUser.Items);
            HasNextPage = listUser.HasNextPage;
            PageNumber += 1;
            IsBusy = false;
        }
        [ICommand]
        public async Task GoToChatDetail(UserModel user)
        {   
            string userId =await  SecureStorage.GetAsync("profile");
            string api = $"api/v1/user/{userId}/conversation/{user.Id.ToString()}";
            var conversation = await _httpClient.GetAsync<ConversationModel>(api);
            await Shell.Current.GoToAsync(nameof(ChatDetailView), true, new Dictionary<string, object>()
            {
                {"ConversationId",conversation.ConversationId },
                {"OtherUser", user }
            });
           
        }
        

      

    }
}
