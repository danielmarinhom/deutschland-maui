using CommunityToolkit.Maui;
using Deutschland_Game.Service;
using Deutschland_Game.View;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Plugin.Maui.Audio;
using Sharpnado.MaterialFrame;
using SkiaSharp.Views.Maui.Controls.Hosting;

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
                .UseSkiaSharp()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("PeachCake.otf", "PeachCake");
                    fonts.AddFont("MUTHIARA DEMO VERSION.OTF", "Muthiara");
                });


            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddTransient<AudioService>();

            builder.Services.AddTransient<EndPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CreditosPage>();
            builder.Services.AddTransient<EscolherNomePage>();
            builder.Services.AddTransient<JogarPage>();


#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddHttpClient();
#endif

            return builder.Build();
        }
    }
}
