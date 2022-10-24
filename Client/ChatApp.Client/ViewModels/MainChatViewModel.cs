
using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Client.Services;
using ChatApp.Client.DataStructures;
using ChatApp.Client.Models;
using ChatApp.Client.Views;
using Microsoft.Toolkit;
using CommunityToolkit.Mvvm.Input;
using ChatApp.Client.Hubs;
using Microsoft.Toolkit.Mvvm.Input;
using ChatApp.Client.Contracts;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChatApp.Client.ViewModels
{   
    public partial class MainChatViewModel:BaseViewModel
    {

        private  HttpClientService _httpClient;

        private readonly IChatHub _chathub;
        
        public MainChatViewModel(IChatHub chathub, IUserOperationHub operation) 
        {
            _httpClient = new HttpClientService();
            _chathub = chathub;
            GetBoxChatModels();
            _chathub.AddMessageHandler(OnReceiveMessage);

            Task.Run(async () =>
            {
                await _chathub.Connect();
                await GetProfileUser();
            });
            
        }
         [RelayCommand]
        public async  Task LoadMoreConversationAsync()
        {  
            if (IsBusy==true)
                return;
            IsBusy = true;          
            var userId = await SecureStorage.GetAsync("profile");
            var item = await _httpClient.GetAsync<RangeObservableCollection<BoxChatModel>>($"api/v1/user/{userId}/conversation?CountConversation={BoxChatModels.Count}&RowFetch=10");
            BoxChatModels.AddRange(item);
            IsBusy = false;
            
        }
        public async Task GetProfileUser()
        {
            var userId = await SecureStorage.GetAsync("profile");
            User = await _httpClient.GetAsync<UserProfileModel>($"api/v1/user/{userId}");

        }
        public   void GetBoxChatModels()
        {
            if (IsBusy == true)
                return;
            IsBusy = true;
            string userId = SecureStorage.GetAsync("profile").Result;
            BoxChatModels = _httpClient.GetAsync<RangeObservableCollection<BoxChatModel>>($"api/v1/user/{userId}/conversation?CountConversation=0&RowFetch=10").Result;
            IsBusy = false;
        }
        [RelayCommand]
        public async void GoToConversationDetail(BoxChatModel param)
        { 
            
            await Shell.Current.GoToAsync(nameof(ChatDetailView), true, new Dictionary<string, object>()
            {
                {"ConversationId",param.ConversationId },
                {"OtherUser", param.User }
            });
            
        }
        private void OnReceiveMessage(MessageModel message)
        {
        }



        [ObservableProperty]
        private RangeObservableCollection<BoxChatModel> boxChatModels;

        [ObservableProperty]
        private UserProfileModel user;



    }
    
}