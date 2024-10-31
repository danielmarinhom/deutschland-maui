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

            _audioService = new AudioService();

            MainPage = new AppShell();


        }
        //protected override async void OnStart() // toca ao iniciar 
        //{
        //    await _audioService.PlayBackgroundAudio();
        //}

        //protected override async void OnResume() // volta quando ele volta pro primeiro plano
        //{
        //    await _audioService.PlayBackgroundAudio();
        //}
    }
}
