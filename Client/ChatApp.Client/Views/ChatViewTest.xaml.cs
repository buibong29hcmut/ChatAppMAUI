using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class ChatViewTest : ContentPage
{
	public ChatViewTest(MainChatViewModel viewmodel)
    {
        BindingContext = viewmodel;
        InitializeComponent();
   
    }
}