using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using Deutschland_Game.Service;

namespace Deutschland_Game.Models.ViewModels
{
    public partial class JogarPageViewModel : ObservableObject 
    {
        private readonly ConquistaUsuarioService conquistaUsuarioService;

        public JogarPageViewModel() { 
            conquistaUsuarioService = new ConquistaUsuarioService();
        } 

        [ObservableProperty]
        private string localImagePath;

        [ObservableProperty]
        private string personagemImagePath;

        [ObservableProperty]
        private string popularidadeText;

        [ObservableProperty]
        private string diplomaciaText;

        [ObservableProperty]
        private string dinheiroText;

        [ObservableProperty]
        private string igrejaText;

        [ObservableProperty]
        private string exercitoText;


        public void setEraPathInImage(string imagePath)
        {
            LocalImagePath = imagePath;
        }

        public void setPersonagemPathInImage(string imagePath)
        {
            PersonagemImagePath = imagePath;
        }

        [RelayCommand]
        public async Task GetAllConquistasByUserID(int userID)
        {

            var response = await conquistaUsuarioService.FindByUserID(userID);

            if(response == null)
            {
                return;
            }

            PopularidadeText = response[0].ValorAcrescentado.ToString();
            IgrejaText = response[1].ValorAcrescentado.ToString();
            DiplomaciaText = response[2].ValorAcrescentado.ToString();
            DinheiroText = response[3].ValorAcrescentado.ToString();
            ExercitoText = response[4].ValorAcrescentado.ToString();

        }

        public async Task UpdateConquistas(List<ConquistasResponseDto> conquistaUsuarioResponses) 
        {
            
        }

    }
}
