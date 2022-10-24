using ChatApp.Client.Contracts;
using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class LoginView : ContentPage
{
	
    public LoginView(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}