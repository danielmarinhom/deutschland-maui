using Deutschland_Game.Service;
using Deutschland_Game.View;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Deutschland_Game
{
    public partial class App : Application
    {
        private AudioService _audioService;
        public App()
        {
            InitializeComponent();

            //Handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

            MainPage = new AppShell();
            //MainPage = new EndPage([300, 200, 100, 50, 20]);


        }
    }
}
