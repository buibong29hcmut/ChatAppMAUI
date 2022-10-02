using ChatApp.Client.Views;

namespace ChatApp.Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ChatViewTest), typeof(ChatViewTest));

            Routing.RegisterRoute(nameof(ChatDetailView), typeof(ChatDetailView));

        }
    }
}