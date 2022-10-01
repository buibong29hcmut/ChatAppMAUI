using ChatApp.Client.Contracts;
using ChatApp.Client.DataStructures;
using ChatApp.Client.Models;
using ChatApp.Client.Services;
using ChatApp.Share.Wrappers;
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
      
        private Guid conversationId;

        public Guid ConversationId
        {
            get => conversationId;
            set
            {
                SetProperty(ref conversationId, value);
                Task.Run(async () =>
                {
                    await GetMessageInitial();
                });
            }
        }
        public ConversationDetailViewModel()
        {
            _httpClient = new HttpClientService();
        }
        private int PageNumber = 1;
        private bool HasNextPage=false;
        public async Task GetMessageInitial()
        {
            string api = $"api/v1/conversation/{ConversationId}/messages?pageSize=10&PageNumber={PageNumber}";
           var result=await _httpClient.GetAsync<PageList<MessageModel>>(api);
            Messages = new(result.Items);
            HasNextPage = result.HasNextPage;
        }
        public async Task LoadMoreMessageCommand()
        {
            await Task.Delay(300);
        }
  
    }
}
