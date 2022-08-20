using Client.MaUI.ViewModels;

namespace Client.MaUI.Views;

public partial class ChatViewTest : ContentPage
{
	public ChatViewTest()
	{
		InitializeComponent();
        BindingContext = new MainChatViewModel();
    }
}