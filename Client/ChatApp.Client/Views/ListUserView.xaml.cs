using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class ListUserView : ContentPage
{
	public ListUserView(ListUserViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}