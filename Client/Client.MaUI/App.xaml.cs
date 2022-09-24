using Client.MaUI.Views;

namespace Client.MaUI
{
    public partial class App : Application
    {
        public App(ChatViewTest view)
        {
            InitializeComponent();

            MainPage = view;
        }
    }
}