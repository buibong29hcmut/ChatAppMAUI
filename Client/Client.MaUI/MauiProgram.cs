
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
            builder.Services.AddScoped<HttpClient>(p =>
            {
                var httpclient = new HttpClient(new HttpsClientHandlerService().GetPlatformMessageHandler());
                httpclient.BaseAddress = new Uri("https://10.0.2.2:7028");
                return httpclient;
            });
            builder.Services.AddScoped<MainChatViewModel>();
            builder.Services.AddScoped<ListUserViewModel>();
            builder.Services.AddScoped<ChatViewTest>();
            builder.Services.AddSingleton<ListUserView>();

            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddScoped<App>();
            return builder.Build();
        }
    }
}