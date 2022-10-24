using ChatApp.Client.Contracts;
using ChatApp.Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public partial class RegisterUserViewModel: BaseViewModel
    {
        private readonly IAuthenticateClientService _auth;
        public RegisterUserViewModel(IAuthenticateClientService auth)
        {
            _auth = auth;
        }
     
        [RelayCommand]
        private async Task RegisterUserAsync()
        {
             await _auth.RegisterAsync(new RegisterUserModel()
            {
                UserName= UserName,
                Name= Name,
                Password=PassWord
            });
            App.Current.MainPage = new AppShell();
        }

        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string passWord;
    }
}
