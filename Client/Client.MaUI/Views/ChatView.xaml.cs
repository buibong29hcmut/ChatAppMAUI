using Client.MaUI.ViewModels;
namespace Client.MaUI.Views;

public partial class ChatView : ContentPage
{
	public ChatView( MainChatViewModel mainchatView)
	{
		InitializeComponent();
		BindingContext = mainchatView;	
    }
}