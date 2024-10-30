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
        //puxar id do cadastro da api e passar no JogarPage no parametro do Navigation
        private UsuarioDto usuarioDto;

        private AllDatasBeforeEraService allDatasBeforeEraService;

        private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses;

        private EraService eraService;

        private PersonagemService personagemService;

        private EraResponse eraResponse;

        private string eraImagePath;

        private List<string> personagemImagesPaths;

        private int eraID;

        public LoadingPage(UsuarioDto usuarioDto, EraResponse eraResponse, int eraID)
        {
            InitializeComponent();
            this.usuarioDto = usuarioDto;

            this.allDatasBeforeEraService = new AllDatasBeforeEraService();

            this.eraService = new EraService();

            this.personagemService = new PersonagemService();

            this.personagemImagesPaths = new List<string>();

            this.eraResponse = eraResponse;

            this.eraID = eraID;

            eraNameLabel.Text = eraResponse.Nome;
            eraPeriodo.Text = eraResponse.Periodo;

            SimulateLoading();
        }


        private async Task isLoadAllDatasBeforeEraSucess()
        {

            var response = await allDatasBeforeEraService.GetDatasByEraID(this.eraID);
            if (response == null)
            {
                await DisplayAlert("Erro", "Algo deu errado! Cadastre o usuário novamente.", "OK");
                await Navigation.PopAsync();
                return;
            }

            this.allDatasBeforeEraResponses = response;

        }

        public async Task LoadEraData()
        {
            eraImagePath = await eraService.DownloadImg64Async(eraResponse.Sprite, eraResponse.Nome);
            Debug.WriteLine(eraImagePath);
        }

        public async Task LoadAllPersonagemData()
        {

            foreach (var data in allDatasBeforeEraResponses)
            {
                var imagePath = await personagemService.DownloadPersonagemImg64Async(data.Personagem.Sprite, data.Personagem.Nome);
                Debug.WriteLine(imagePath);
                personagemImagesPaths.Add(imagePath);
            }

        }


        private async void SimulateLoading()
        {
            for (double progress = 0; progress <= 400; progress += 40)
            {
                ProgressBox.WidthRequest = progress;
                progressLabel.Text = $"{(int)(progress/40*10)}%";

                if(progress == 80)
                {
                    await isLoadAllDatasBeforeEraSucess();
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

            await Navigation.PushAsync(new JogarPage(this.usuarioDto, this.allDatasBeforeEraResponses, eraImagePath, personagemImagesPaths));
        }
        
    }

}
