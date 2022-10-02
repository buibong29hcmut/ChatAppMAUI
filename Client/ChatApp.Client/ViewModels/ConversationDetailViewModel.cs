using ChatApp.Client.Contracts;
using ChatApp.Client.DataStructures;
using ChatApp.Client.Models;
using ChatApp.Client.Services;
using ChatApp.Share.Wrappers;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
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

        public RangeObservableCollection<MessageModel> Messages { get; private set; } 
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
        public async Task GetMessages()
        {
            string api = $"api/v1/conversation/{ConversationId}/messages?pageSize=20&PageNumber={PageNumber}";
            var result =  await _httpClient.GetAsync<PageList<MessageModel>>(api);
            Messages = new(result.Items);
            HasNextPage = result.HasNextPage;
        }
        public void GetMessagesInitial()
        {
            Task.Run(async () =>
            {
                IsRefreshing = true;
                await GetMessages();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsRefreshing = false;
            });

        }
     
        public async Task LoadMoreMessageCommand()
        {
            await Task.Delay(300);
            
        }
       
  
    }
}
