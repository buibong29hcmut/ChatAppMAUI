using Client.MaUI.DataStructures;
using Client.MaUI.Models;
using Client.MaUI.Views;
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

namespace Client.MaUI.ViewModels
{   
    public partial class MainChatViewModel:BaseViewModel
    {
        
        public RangeObservableCollection<string> urlUserOnline { get; } = new();
        public RangeObservableCollection<BoxChatModel> BoxChatModels { get; private set; } = new();
        private readonly HttpClient _httclient;
        public MainChatViewModel(HttpClient httpclient) 
        {
            _httclient = httpclient;
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
        public async void LoadMoreConversation()
        {
            if (IsBusy==true)
                return;
            IsBusy = true;
            var response =await _httclient.GetAsync($"api/v1/user/105c659e-abd1-4821-9d1d-905ccd5a9e87/conversation?CountConversation={BoxChatModels.Count}&RowFetch=10");
            var content = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(content)["data"].ToString();
            var item = JsonConvert.DeserializeObject<RangeObservableCollection<BoxChatModel>>(data);
            BoxChatModels.AddRange(item);
            IsBusy = false;

        }
        public  void GetBoxChatModels()
        {
          var response=    _httclient.GetAsync("api/v1/user/105c659e-abd1-4821-9d1d-905ccd5a9e87/conversation?CountConversation=0&RowFetch=10").Result;
            var content =  response.Content.ReadAsStringAsync().Result;
            var data = JObject.Parse(content)["data"].ToString();
            BoxChatModels = JsonConvert.DeserializeObject<RangeObservableCollection<BoxChatModel>>(data);
        }
        [ICommand]
        public  void GetChatDetailView()
        {
            App.Current.MainPage = new NavigationPage(new ChatDetailView());
        }
        
    }
    
}
