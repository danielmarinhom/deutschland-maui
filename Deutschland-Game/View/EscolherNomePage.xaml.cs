using Deutschland_Game.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Deutschland_Game.ViewModel;
using System.Diagnostics;

namespace Deutschland_Game.View
{
    public partial class EscolherNomePage : ContentPage, INotifyPropertyChanged
    {
        private string nomeRei;
        private CadastrarUsuarioViewModel cadastrarUsuarioViewModel;
        private EraService eraService;
        private AudioService audioService;
        private bool wasButtonClicked = false;


        public string NomeRei
        {
            get => nomeRei;
            set{
                if (nomeRei != value){ nomeRei = value;  OnPropertyChanged(); }
            }
        }

        public EscolherNomePage(AudioService audioService)
        {
            InitializeComponent();
            BindingContext = this;
            NomeRei = "Francisco Barbarossa";
            cadastrarUsuarioViewModel = new CadastrarUsuarioViewModel();
            eraService = new EraService();
            this.audioService = audioService;
        }
        // update left scroll according to right entry irt
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void continuarBtn_Clicked(object sender, EventArgs e)
        {
            if (wasButtonClicked) return;

            wasButtonClicked = true;

            audioService.PlayClickAudio();
            loading_component.IsVisible = true; // habilita o loading visual
            try { 

                var usuarioDto = await cadastrarUsuarioViewModel.CadastrarUsuario(nomeRei);

                var eraResponse = await eraService.GetEraByID(1);

                loading_component.IsVisible = false; // desabilita o loading visual

                if (usuarioDto == null) {
                    await DisplayAlert("Erro", "Falha ao cadastrar o usu�rio", "OK");
                }
                else
                {
                    audioService.StopBackGroundAudio();
                    await Navigation.PushAsync(new LoadingPage(usuarioDto, eraResponse, audioService));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERRO ------------ " + ex.Message);
            }

            await Task.Delay(1000);
            wasButtonClicked = false;
        }

        private async void voltarBtn_Clicked(object sender, EventArgs e)
        {
            audioService.PlayClickAudio();
            await Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            audioService.PlayClickAudio();
            Navigation.PopAsync();
            return true;
        }

        private void nameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(nameEntry.Text.Length >= 22)
            {
                nameEntry.Text = nameEntry.Text.Substring(0, 22);
            }
        }
    }
}