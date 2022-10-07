
using ChatApp.Client.Services;
using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class ChatDetailView : ContentPage
{

    public ChatDetailView(ConversationDetailViewModel vm )
    {
 
            InitializeComponent();
            BindingContext = vm;


    }

    private  void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (this.BindingContext as ConversationDetailViewModel).GetMessagesInitialCommand.Execute(null);

    }
}