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
        [ObservableProperty]
        private int pageNumber = 1;
        [ObservableProperty]
        private bool hasNextPage = true;
        private  void GetMessages()
        {
            string api = $"api/v1/conversation/{conversationId}/messages?pageSize=20&PageNumber={PageNumber}";
            var result =   _httpClient.GetAsync<PageList<MessageModel>>(api).Result;
            Messages = new(result.Items);
            HasNextPage = result.HasNextPage;
            PageNumber += 1;



        }
        [RelayCommand]
        public void GetMessagesInitial()
        {
              GetMessages();
        }
        [RelayCommand]
        public async Task LoadMoreMessage()
        {
            await Task.Run(() =>
             {
                 if (!HasNextPage || IsBusy == true)
                     return;
                 IsBusy = true;
                 GetMessages();
                 IsBusy = false;
             });
            
            
        }
       
  
    }
}
