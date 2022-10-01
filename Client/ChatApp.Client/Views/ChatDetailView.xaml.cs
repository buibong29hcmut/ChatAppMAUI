using ChatApp.Client.Services;
using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class ChatDetailView : ContentPage
{ 

	public ChatDetailView()
    {
        BindingContext = new ConversationDetailViewModel();
        InitializeComponent();

    }

}