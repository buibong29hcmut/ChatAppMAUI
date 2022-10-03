using ChatApp.Client.Contracts;
using ChatApp.Client.DataStructures;
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
        private bool isRefreshing;
        public ConversationDetailViewModel()
        {
            _httpClient = new HttpClientService();
            Messages = new();
            
        }
        private int PageNumber = 1;
        private bool HasNextPage = false;
        private void GetMessages()
        {
            string api = $"api/v1/conversation/{conversationId}/messages?pageSize=20&PageNumber={PageNumber}";
            var result =   _httpClient.GetAsync<PageList<MessageModel>>(api).Result;
            Messages = new(result.Items);
            HasNextPage = result.HasNextPage;
            
            
        }
        [RelayCommand]
        public void GetMessagesInitial()
        {
          
                IsRefreshing = true;
                GetMessages();
                IsRefreshing = false;
            

        }
     
        public async Task LoadMoreMessageCommand()
        {
            await Task.Delay(300);
            
        }
       
  
    }
}
