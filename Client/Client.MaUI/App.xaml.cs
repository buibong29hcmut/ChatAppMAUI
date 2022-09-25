using Client.MaUI.Views;

namespace Client.MaUI
{
    public partial class App : Application
    {
        public App(LoginView view)
        {
            InitializeComponent();

            var token = SecureStorage.GetAsync("chattoken").Result;
            if (string.IsNullOrEmpty(token))
            {
                MainPage = view;
                return;
            }
            MainPage = new AppShell();
        }
    }
}