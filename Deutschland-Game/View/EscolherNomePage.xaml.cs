using Deutschland_Game.Models;
using Deutschland_Game.Service;
using Deutschland_Game.Dtos;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Deutschland_Game.ViewModel;

namespace Deutschland_Game.View
{
    public partial class EscolherNomePage : ContentPage, INotifyPropertyChanged
    {
        private string nomeRei;
        private CadastrarUsuarioViewModel cadastrarUsuarioViewModel;
        public string NomeRei
        {
            get => nomeRei;
            set{
                if (nomeRei != value){ nomeRei = value;  OnPropertyChanged(); }
            }
        }

        public EscolherNomePage()
        {
            InitializeComponent();
            BindingContext = this;
            NomeRei = "Francisco Barbarossa";
        }
        // update left scroll according to right entry irt
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void continuarBtn_Clicked(object sender, EventArgs e)
        {
            double id = await cadastrarUsuarioViewModel.CadastrarUsuario(nomeRei);
            if (id == -1) {
                // tratar erro (ver com o pscosta)
                await DisplayAlert("Erro", "Falha ao cadastrar o usuário", "OK");
            }
            else
            {
                await Navigation.PushAsync(new LoadingPage(id));
            }
        }

        private async void voltarBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync();
            return true;
        }
    }
}