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
            eraService = new EraService();
        }
        // update left scroll according to right entry irt
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void continuarBtn_Clicked(object sender, EventArgs e)
        {

            loading_component.IsVisible = true; // habilita o loading visual
            try { 

                var usuarioDto = await cadastrarUsuarioViewModel.CadastrarUsuario(nomeRei);

                var eraResponse = await eraService.GetEraByID(1);

                loading_component.IsVisible = false; // desabilita o loading visual

                if (usuarioDto == null) {
                    // tratar erro (ver com o pscosta) - por mim tá ok, nao precisa ter trabalho com isto
<<<<<<< HEAD
                    // kkkkkkkkkkkkkk okk
=======
>>>>>>> 3edfa5af167a3f775c65a3b047b4f0e203e4311c
                    await DisplayAlert("Erro", "Falha ao cadastrar o usuário", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new LoadingPage(usuarioDto, eraResponse, 1));
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