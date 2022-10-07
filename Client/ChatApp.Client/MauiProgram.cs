
using ChatApp.Client.Contracts;
using ChatApp.Client.Services;
using ChatApp.Client.ViewModels;
using ChatApp.Client.Views;
using Client.MaUI.Views;


namespace ChatApp.Client
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
            builder.Services.AddSingleton<IHttpClientService,HttpClientService>();
            builder.Services.AddSingleton<IAuthenticateClientService, AuthenticateClientService>();
            builder.Services.AddSingleton<MainChatViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddScoped<ListUserViewModel>();
            builder.Services.AddTransient<ConversationDetailViewModel>();
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<ChatViewTest>();
            builder.Services.AddSingleton<ListUserView>();
            builder.Services.AddTransient<ChatDetailView>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<App>();
            return builder.Build();
        }
    }
}