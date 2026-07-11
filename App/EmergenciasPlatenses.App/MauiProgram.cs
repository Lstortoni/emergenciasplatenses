using EmergenciasPlatenses.App.Services;
using Microsoft.Extensions.Logging;

namespace EmergenciasPlatenses.App;

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
            });

        builder.Services.AddMauiBlazorWebView();

#if ANDROID
        var apiBaseAddress = "http://10.0.2.2:5112/";
#else
        var apiBaseAddress = "http://localhost:5112/";
#endif

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri(apiBaseAddress)
        });
        builder.Services.AddSingleton<IEmergenciasApiClient, EmergenciasApiClient>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
