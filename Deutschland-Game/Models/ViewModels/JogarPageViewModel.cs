using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace Deutschland_Game.Models.ViewModels
{
    public class JogarPageViewModel : INotifyPropertyChanged
    {
        public string LocalImagePath { get; set; }
        public string PersonagemImagePath { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
