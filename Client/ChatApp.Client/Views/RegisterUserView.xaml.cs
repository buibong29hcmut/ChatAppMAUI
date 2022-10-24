using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class RegisterUserView : ContentPage
{
	public RegisterUserView(RegisterUserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}