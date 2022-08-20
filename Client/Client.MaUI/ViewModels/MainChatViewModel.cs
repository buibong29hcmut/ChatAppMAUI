using Client.MaUI.Models;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
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
        
        public ObservableCollection<string> urlUserOnline { get; } = new();
        public ObservableCollection<BoxChatModel> BoxChatModels { get; } = new();
        public MainChatViewModel() 
        {   
            GetUserOnline();
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
        public void GetBoxChatModels()
        {
            if (urlUserOnline.Count>0)
            {
                foreach (var item in urlUserOnline)
                {
                    BoxChatModels.Add(new BoxChatModel()
                    {
                        UrlImage = item
                    });
                }
            }
        }
        
    }
    
}
