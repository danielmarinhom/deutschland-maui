using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility;

namespace Deutschland_Game
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Coloca o app em tela horizontal
            RequestedOrientation = ScreenOrientation.Landscape;

            Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                SystemUiFlags.ImmersiveSticky | // isso permite que ambos os itens anteriores desapareçam automaticamente após uma interação do usuario
                SystemUiFlags.LayoutFullscreen |
                SystemUiFlags.LayoutHideNavigation |
                SystemUiFlags.Fullscreen | // deixa o app em tela cheia e esconde a status bar
                SystemUiFlags.HideNavigation ); // esconde aquela aba de navegacao do celular
            

            Window.DecorView.SetOnSystemUiVisibilityChangeListener(new SystemUiVisibilityChangeListener()); // aqui é um listener para executar os comandos acima toda vez que houver alteração da view do app

        }

        public class SystemUiVisibilityChangeListener : Java.Lang.Object, Android.Views.View.IOnSystemUiVisibilityChangeListener 
        {


            public void OnSystemUiVisibilityChange(StatusBarVisibility visibility)
            {
                if ((visibility & (StatusBarVisibility)SystemUiFlags.Fullscreen) == 0)
                {
                    var decorView = ((Activity)Platform.CurrentActivity).Window.DecorView;
                    decorView.SystemUiVisibility = (StatusBarVisibility)(
                        SystemUiFlags.HideNavigation |
                        SystemUiFlags.Fullscreen |
                        SystemUiFlags.ImmersiveSticky |
                        SystemUiFlags.LayoutFullscreen |
                        SystemUiFlags.LayoutHideNavigation);
                }
            }
        }

        
    }
}
