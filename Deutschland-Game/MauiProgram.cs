using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Sharpnado.MaterialFrame;

namespace Deutschland_Game
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSharpnadoMaterialFrame(loggerEnable: false)
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("PeachCake.otf", "PeachCake");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddHttpClient();
#endif

            return builder.Build();
        }
    }
}
