using CommunityToolkit.Mvvm.ComponentModel;
using Deutschland_Game.Models.ApiModels;

namespace Deutschland_Game.Models.ViewModels
{
    public partial class GamePageViewModel : ObservableObject
    {

        [ObservableProperty]
        private List<LoadingResponseDto> loadingResponses;

    }
}
