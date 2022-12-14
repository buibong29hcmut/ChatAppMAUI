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
    [QueryProperty("OtherUser", "OtherUser")]
    public partial class ConversationDetailViewModel:BaseViewModel
    {
        private readonly IHttpClientService _httpClient;
        private readonly IChatHub _chathub;
        private readonly IUserOperationHub _operation;
        public ConversationDetailViewModel(IChatHub chathub)
        {   
            _httpClient = new HttpClientService();
            _chathub = chathub;
            _chathub.AddMessageHandler(OnReceiveMessage);
            Messages = new();
            Task.Run(async () => 
            {
                await _chathub.Connect();
           });
            
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
                SendTime = currentTime,
            };

            await _chathub.SendMessageToConversation(new MessageForSendConversation()
            {
                ConversationId= ConversationId,
                SendTime= currentTime,
                Content=Message,
                ToUserId= OtherUser.Id,
            });

            Messages.Add(new MessageModel()
            {
                FromUserId = new Guid(userId),
                Content = Message,
                SendTime = DateTimeOffset.Now,

            });
            Message = "";
            
        }
        [ObservableProperty]
        private RangeObservableCollection<MessageModel> messages;

        [ObservableProperty]
        private Guid conversationId;

        [ObservableProperty]
        private Guid otherUserId;

        [ObservableProperty]
        private bool isRefreshing = false;

        [ObservableProperty]
        private string message;

        [ObservableProperty]
        private UserModel otherUser;
    }
}
