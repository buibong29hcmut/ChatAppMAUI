
using Client.MaUI.Contracts;
using Client.MaUI.Services;
using Client.MaUI.ViewModels;
using Client.MaUI.Views;


namespace Client.MaUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Metropolis-Black.otf", "Metropolis Black");
                    fonts.AddFont("Metropolis-Light.otf", "Metropolis Light");
                    fonts.AddFont("Metropolis-Medium.otf", "Metropolis Medium");
                    fonts.AddFont("Metropolis-Regular.otf", "Metropolis Regular");
                    fonts.AddFont("Metropolis-Regular.otf", "Metropolis Regular");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "Material Icons");
                });
            builder.Services.AddSingleton<HttpClientService>();
            builder.Services.AddSingleton<IAuthenticateClientService, AuthenticateClientService>();
            builder.Services.AddSingleton<MainChatViewModel>();
            builder.Services.AddSingleton<ListUserViewModel>();
            Routing.RegisterRoute("Login", typeof(LoginView));
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<ChatViewTest>();
            builder.Services.AddSingleton<ListUserView>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<App>();
            return builder.Build();
        }
    }
}