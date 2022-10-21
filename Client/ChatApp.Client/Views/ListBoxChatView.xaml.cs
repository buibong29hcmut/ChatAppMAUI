using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class ListBoxChatView : ContentPage
{
	public ListBoxChatView(MainChatViewModel viewmodel)
    {
        BindingContext = viewmodel;
        InitializeComponent();
   
    }
}