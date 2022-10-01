using ChatApp.Client.Views;
using Client.MaUI.Views;

namespace ChatApp.Client
{
    public partial class App : Application
    {
        public App(LoginView view)
        {
            InitializeComponent();

            MainPage = view;
        }
    }
}