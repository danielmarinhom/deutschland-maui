using Deutschland_Game.Models;
using Deutschland_Game.Service;
using Deutschland_Game.Dtos;
using Microsoft.Maui.Controls.PlatformConfiguration;
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
            cadastrarUsuarioViewModel = new CadastrarUsuarioViewModel();
        }
        // update left scroll according to right entry irt
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void continuarBtn_Clicked(object sender, EventArgs e)
        {
            loading_component.IsVisible = true;
            try { 
                var usuarioDto = await cadastrarUsuarioViewModel.CadastrarUsuario(nomeRei);
                loading_component.IsVisible = false;
                if (usuarioDto == null) {
                    // tratar erro (ver com o pscosta)
                    await DisplayAlert("Erro", "Falha ao cadastrar o usuário", "OK");
                }
                else
                {

                    var globalVars = new GlobalVars();

                    globalVars.eraID = 1;

                    await Navigation.PushAsync(new LoadingPage(usuarioDto, globalVars.eraID));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERRO ------------ " + ex.Message);
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