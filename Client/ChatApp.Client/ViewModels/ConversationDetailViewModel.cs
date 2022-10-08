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
        private bool isRefreshing=false;
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
            string api = $"api/v1/conversation/{conversationId}/messages?pageSize=30&PageNumber={PageNumber}";
            var result =    _httpClient.GetAsync<PageList<MessageModel>>(api).Result;
            if (Messages.Count > 0)
                Messages.InsertRange(0,result.Items);
            else
            {
                Messages = new(result.Items);
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
       
  
    }
}
