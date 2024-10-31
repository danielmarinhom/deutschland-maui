using Deutschland_Game.Service;
using Deutschland_Game.View;
using System.Diagnostics;
using System.Text.Json;

namespace Deutschland_Game
{
    public partial class MainPage : ContentPage
    {
        private readonly AudioService audioService;

        public MainPage(AudioService audioService)
        {
            InitializeComponent();
            this.audioService = audioService;
            audioService.PlayBackgroundAudio();
        }

        private async void creditosBtn_Clicked(object sender, EventArgs e)
        {
            audioService.PlayClickAudio();
            await Navigation.PushAsync(new CreditosPage(audioService));
        }

        private async void jogarBtn_Clicked(object sender, EventArgs e)
        {
            audioService.PlayClickAudio();
            await Navigation.PushAsync(new EscolherNomePage(audioService));
        }
    }

}
