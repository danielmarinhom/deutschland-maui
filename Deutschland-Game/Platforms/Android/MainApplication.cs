using Android.App;
using Android.Content.Res;
using Android.Runtime;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace Deutschland_Game
{
    #if DEBUG
        [Application (UsesCleartextTraffic = true)]
    #else
        [Application]
    #endif
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
                if (view is Entry)
                {
                    // remove underline entry
                    handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
                    handler.PlatformView.TextCursorDrawable.SetTint(Color.FromRgb(173, 140, 101).ToAndroid());

                }
            });

        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
