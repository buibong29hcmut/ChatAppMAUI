
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Share;
using ChatApp.Share.Wrappers;
using Microsoft.Toolkit.Mvvm.Input;
using ChatApp.Client.DataStructures;
using ChatApp.Client.Models;
using ChatApp.Client.Services;

namespace ChatApp.Client.ViewModels
{
    public partial class ListUserViewModel:BaseViewModel
    {
        private readonly HttpClientService _httpClient;
        public RangeObservableCollection<UserModel> Users { get; private set; } = new();
        private int PageNumber = 1;
        bool HasNextPage;
        public ListUserViewModel()
        {
            _httpClient= new HttpClientService();
            GetListUser();
            
        }
        public  void GetListUser()
        {           
            var data =  _httpClient.GetAsync<PageList<UserModel>>($"api/v1/user?pageSize=10&pageNumber={PageNumber}").Result;     
            Users.AddRange(data.Items);
            HasNextPage = data.HasNextPage;
        }
        [ICommand]
        public async void LoadMoreUserAsync()
        {
            if (IsBusy==true|| HasNextPage==false)
                return;
            IsBusy = true;
            var listUser = await _httpClient.GetAsync<PageList<UserModel>>($"api/v1/user?pageSize=10&pageNumber={PageNumber+1}");     
            Users.AddRange(listUser.Items);
            HasNextPage = listUser.HasNextPage;
            PageNumber += 1;
            IsBusy = false;
        }
      

    }
}
