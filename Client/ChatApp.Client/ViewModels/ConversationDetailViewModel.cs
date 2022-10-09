using ChatApp.Client.Contracts;
using ChatApp.Client.DataStructures;
using ChatApp.Client.Hubs;
using ChatApp.Client.Models;
using ChatApp.Client.Services;
using ChatApp.Share.Wrappers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChatApp.Client.ViewModels
{
    [QueryProperty("ConversationId", "ConversationId")]
    public partial class ConversationDetailViewModel:BaseViewModel
    {
        [ObservableProperty]
        private RangeObservableCollection<MessageModel> messages;
        private readonly IHttpClientService _httpClient;
        [ObservableProperty]
        private Guid conversationId;
        [ObservableProperty]
        private bool isRefreshing=false;
        private readonly ChatHub _chathub;
        [ObservableProperty]
        private string message;
        public ConversationDetailViewModel(ChatHub chathub)
        {
            _httpClient = new HttpClientService();
            _chathub = chathub;
            Messages = new();
            Task.Run(async () => await _chathub.Connect());
            
        }
        [ObservableProperty]
        private int pageNumber = 1;
        [ObservableProperty]
        private bool hasNextPage = true;
        private  void GetMessages()
        {
            string api = $"api/v1/conversation/{conversationId}/messages?pageSize=30&PageNumber={PageNumber}";
            var result =    _httpClient.GetAsync<PageList<MessageModel>>(api).Result;
            var items = result.Items;
            if (result != null)
                items.Reverse();
            if (Messages.Count > 0)
                Messages.InsertRange(0,result.Items);
            else
            {
                Messages = new(items);
            }
            HasNextPage = result.HasNextPage;
            PageNumber += 1;



        }
        [RelayCommand]
        public  void GetMessagesInitial()
        {
            IsBusy = true;
             GetMessages();
              IsBusy = false;
        }
        [RelayCommand]
        public async Task LoadMoreMessage()
        {

            if (HasNextPage == false)
            {
                IsRefreshing = false;
                return;
            }
                IsRefreshing = true;
                 GetMessages();
            IsRefreshing = false;
             await Task.CompletedTask;
        }
        private void OnReceiveMessage(MessageModel message )
        {
            Messages.Add(message);
        }
        [RelayCommand]
        private async Task SendMessage()
        {
            var userId = await SecureStorage.GetAsync("profile");
            var currentTime = DateTime.Now;
            if (string.IsNullOrEmpty(Message))
                return;
            MessageModel messageModel = new MessageModel()
            {
                FromUserId = new Guid(userId),
                Content = Message,
                IsThisUser = true,
                SendTime = currentTime,
            };

            await _chathub.SendMessageToConversation(new MessageForSendConversation()
            {
                ConversationId= ConversationId,
                SendTime= currentTime,
                Content=Message
            });

            Messages.Add(new MessageModel()
            {
                FromUserId = new Guid(userId),
                Content = Message,
                IsThisUser = true,
                SendTime = DateTimeOffset.Now,

            });
            Message = "";
            
        }

    }
}
