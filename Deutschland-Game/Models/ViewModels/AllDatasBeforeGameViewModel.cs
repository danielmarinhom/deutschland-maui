using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deutschland_Game.Models.ApiModels;
using Deutschland_Game.Service;

namespace Deutschland_Game.Models.ViewModels
{
    public partial class AllDatasBeforeGameViewModel : ObservableObject
    {

        private readonly AllDatasBeforeEraService allDatasBeforeEraService;

        public AllDatasBeforeGameViewModel() { 
        
            allDatasBeforeEraService = new AllDatasBeforeEraService();

        }

        [ObservableProperty]
        private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses;

        [RelayCommand]
        public async Task GetAllDatas(int eraID)
        {
            var response = await allDatasBeforeEraService.GetDatasByEraID(eraID);
            if(response == null)
            {
                Console.WriteLine("OBJETO NULO");
                return;
            }

        }

    }
}
