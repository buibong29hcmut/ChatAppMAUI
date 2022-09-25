using Client.MaUI.ViewModels;

namespace Client.MaUI.Views;

public partial class ListUserView : ContentPage
{
	public ListUserView(ListUserViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}