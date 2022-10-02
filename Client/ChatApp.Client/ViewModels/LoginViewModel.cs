using ChatApp.Client.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public partial class LoginViewModel:BaseViewModel
    {
        private readonly IAuthenticateClientService _http;
        public LoginViewModel(IAuthenticateClientService http)
        {
            _http = http;
            
        }
        [ObservableProperty]
        private string username;
        [ObservableProperty]

        private string password;
        [RelayCommand]
        private async Task LoginAsync()
        {
            IsBusy = true;
            await _http.Login(Username, Password);
            App.Current.MainPage = new AppShell();
            IsBusy = false;
        }
    }
}
