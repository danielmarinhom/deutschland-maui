using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using Deutschland_Game.Service;
using Microsoft.Maui.Controls;

namespace Deutschland_Game.View
{
    public partial class LoadingPage : ContentPage
    {
        private readonly AudioService audioService;

        //puxar id do cadastro da api e passar no JogarPage no parametro do Navigation
        private UsuarioDto usuarioDto;

        private AllDatasBeforeEraService allDatasBeforeEraService;

        private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses;

        private EraService eraService;

        private PersonagemService personagemService;

        private EraResponse eraResponse;

        private string eraImagePath;

        private List<string> personagemImagesPaths;


        public LoadingPage(UsuarioDto usuarioDto, EraResponse eraResponse, AudioService audioService)
        {
            InitializeComponent();

            this.audioService = audioService;
            this.usuarioDto = usuarioDto;

            this.allDatasBeforeEraService = new AllDatasBeforeEraService();

            this.eraService = new EraService();

            this.personagemService = new PersonagemService();

            this.personagemImagesPaths = new List<string>();

            this.eraResponse = eraResponse;

            eraNameLabel.Text = eraResponse.Nome;
            eraPeriodo.Text = eraResponse.Periodo;

            SimulateLoading();
        }


        private async Task LoadAllDatasBeforeEra()
        {

            var response = await allDatasBeforeEraService.GetDatasByEraID(this.eraResponse.Id); // carrega todos os dados dos dialogos, dos personagens e das consequencias
            if (response == null) // se nao deu certo, response == null
            {
                await DisplayAlert("Erro", "Algo deu errado! Cadastre o usuário novamente.", "OK");
                await Navigation.PopAsync();
                return;
            }

            this.allDatasBeforeEraResponses = response; // atribui à variavel global da Classe

        }

        public async Task LoadEraData()
        {
            eraImagePath = await eraService.DownloadImg64Async(eraResponse.Sprite, eraResponse.Nome); // baixa a imagem da era no dispositivo
        }

        public async Task LoadAllPersonagemData()
        {

            foreach (var data in allDatasBeforeEraResponses)
            {
                // baixa a imagem dos personagens no dispositivo
                var imagePath = await personagemService.DownloadPersonagemImg64Async(data.Personagem.Sprite, data.Personagem.Nome); 
                personagemImagesPaths.Add(imagePath); // adiciona à lista
            }

        }


        private async void SimulateLoading()
        {
            for (double progress = 0; progress <= 400; progress += 40)
            {
                ProgressBox.WidthRequest = progress;
                progressLabel.Text = $"{(int)(progress/40*10)}%";

                if(progress == 80) // só pra decoração, simula um carregamento dos requests em diferentes partes do carregamento
                {
                    await LoadAllDatasBeforeEra();
                }

                if(progress == 240)
                {
                    await LoadEraData();
                }

                if(progress == 320)
                {
                    await LoadAllPersonagemData();
                }


                await Task.Delay(500);
            }

            await Navigation.PushAsync(new JogarPage(this.usuarioDto, this.allDatasBeforeEraResponses, eraImagePath, personagemImagesPaths, eraResponse, audioService));
        }

        protected override bool OnBackButtonPressed() // cancela o botao de voltar do celular
        {
            return true;
        }

    }

}
