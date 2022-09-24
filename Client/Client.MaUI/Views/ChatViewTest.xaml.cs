using Client.MaUI.ViewModels;

namespace Client.MaUI.Views;

public partial class ChatViewTest : ContentPage
{
	public ChatViewTest(MainChatViewModel viewmodel)
    {
        BindingContext = viewmodel;
        InitializeComponent();
   
    }
}