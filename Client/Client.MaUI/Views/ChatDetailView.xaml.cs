using Client.MaUI.ViewModels;

namespace Client.MaUI.Views;

public partial class ChatDetailView : ContentPage
{ 

	public ChatDetailView()
	{
		InitializeComponent();
		BindingContext = new ChatDetailViewModel();

    }

}