using ChatApp.Client.Contracts;
using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class LoginView : ContentPage
{
	private readonly IAuthenticateClientService _auth;
    public LoginView(IAuthenticateClientService auth)
	{
		_auth = auth;
		InitializeComponent();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await  _auth.Login(userTxt.Text, passwordTxt.Text);
        App.Current.MainPage = new NavigationPage(new AppShell());
    }
}