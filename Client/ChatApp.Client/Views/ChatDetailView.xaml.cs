using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class ChatDetailView : ContentPage
{ 

	public ChatDetailView()
	{
		InitializeComponent();
		BindingContext = new ChatDetailViewModel();

    }

}