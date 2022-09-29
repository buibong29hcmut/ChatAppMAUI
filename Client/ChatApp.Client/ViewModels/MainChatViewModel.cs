
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Toolkit.Mvvm.Input;
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

namespace ChatApp.Client.ViewModels
{   
    public partial class MainChatViewModel:BaseViewModel
    {
        
        public RangeObservableCollection<string> urlUserOnline { get; } = new();
        public RangeObservableCollection<BoxChatModel> BoxChatModels { get; private set; } = new();
        public string UrlProFileUser { get; set; }
        private  HttpClientService _httpClient;
        public MainChatViewModel() 
        {
            _httpClient = new HttpClientService();
            GetBoxChatModels();
            
            


        }
        public void GetUserOnline()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("563492ad6f91700001000001a5f1a924e439437e87eca4b78639f989");
            var reponse =  httpClient.GetAsync(new Uri(@"https://api.pexels.com/v1/curated?per_page=20")).Result;
            var stringJson =  reponse.Content.ReadAsStringAsync().Result;
            dynamic parseJson = JsonConvert.DeserializeObject(stringJson);
            foreach(var user in parseJson.photos)
            {
                string url = Convert.ToString(user.src.original);
                urlUserOnline.Add(url);
            }
        }
        [ICommand]
        public async Task LoadMoreConversationAsync()
        {
            if (IsBusy==true)
                return;
            IsBusy = true;
            var userId =await SecureStorage.GetAsync("profile");
            var item =await _httpClient.GetAsync<RangeObservableCollection<BoxChatModel>>($"api/v1/user/{userId}/ conversation?CountConversation={BoxChatModels.Count}&RowFetch=10");
            BoxChatModels.AddRange(item);
            IsBusy = false;

        }
        public  void GetBoxChatModels()
        {
            var userId = SecureStorage.GetAsync("profile").Result;

            BoxChatModels = _httpClient.GetAsync<RangeObservableCollection<BoxChatModel>>($"api/v1/user/{userId}/conversation?CountConversation=0&RowFetch=10").Result;
        }
        [ICommand]
        public  void GetChatDetailView()
        {
            App.Current.MainPage = new NavigationPage(new ChatDetailView());
        }
        
    }
    
}
