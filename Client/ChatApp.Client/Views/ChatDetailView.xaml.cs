
using ChatApp.Client.Services;
using ChatApp.Client.ViewModels;

namespace ChatApp.Client.Views;

public partial class ChatDetailView : ContentPage
{

    public ChatDetailView(ConversationDetailViewModel vm )
    {
        BindingContext = vm;
        InitializeComponent();
    }

    private  void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {   
        var vm = this.BindingContext as ConversationDetailViewModel;
        vm.GetMessagesInitialCommand.Execute(null);
        collectionMessageView.ScrollTo(vm.Messages[vm.Messages.Count-1], true);

    }

}