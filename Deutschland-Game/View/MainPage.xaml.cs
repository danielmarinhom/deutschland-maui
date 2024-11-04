using Deutschland_Game.Service;
using Deutschland_Game.View;
using System.Diagnostics;
using System.Text.Json;

namespace Deutschland_Game
{
    public partial class MainPage : ContentPage
    {
        private readonly AudioService audioService;

        private bool wasButtonClicked = false;

        public MainPage(AudioService audioService)
        {
            InitializeComponent();
            this.audioService = audioService;
            audioService.PlayBackgroundAudio();
        }

        private async void creditosBtn_Clicked(object sender, EventArgs e)
        {

            if (wasButtonClicked) { return;  } // evitar duplo clique, q leva a duas páginas ao mesmo tempo

            wasButtonClicked = true;

            audioService.PlayClickAudio();
            await Navigation.PushAsync(new CreditosPage(audioService));

            await Task.Delay(1000);
            wasButtonClicked = false;

        }

        private async void jogarBtn_Clicked(object sender, EventArgs e)
        {
            if (wasButtonClicked) { return; }

            wasButtonClicked = true;

            audioService.PlayClickAudio();
            await Navigation.PushAsync(new EscolherNomePage(audioService));
            await Task.Delay(1000);

            wasButtonClicked = false;

        }

        protected override bool OnBackButtonPressed() // cancela o botao de voltar do celular
        {
            return true;
        }
    }

}
