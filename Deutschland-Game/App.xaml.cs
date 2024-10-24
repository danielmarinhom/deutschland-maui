using Deutschland_Game.View;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Deutschland_Game
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            //Handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);



            MainPage = new AppShell();
            //MainPage = new GamePage();

        }
    }
}
