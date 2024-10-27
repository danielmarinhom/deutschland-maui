using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Deutschland_Game.Models.ViewModels
{
    public partial class JogarPageViewModel : ObservableObject 
    {

        [ObservableProperty]
        private string localImagePath;

        [ObservableProperty]
        private string personagemImagePath;

        public async Task DownloadImg64Async(string base64, string nome)
        {
            byte[] imageBytes = Convert.FromBase64String(base64);
            string fileName = $"{nome}.png";
            string localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            await File.WriteAllBytesAsync(localPath, imageBytes);

            LocalImagePath = localPath;
            OnPropertyChanged(nameof(LocalImagePath));
        }
        public async Task DownloadPersonagemImg64Async(string base64, string nome)
        {
            byte[] imageBytes = Convert.FromBase64String(base64);
            string fileName = $"{nome}_personagem.png";
            string localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            await File.WriteAllBytesAsync(localPath, imageBytes);

            PersonagemImagePath = localPath;
            OnPropertyChanged(nameof(PersonagemImagePath));
        }


        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
