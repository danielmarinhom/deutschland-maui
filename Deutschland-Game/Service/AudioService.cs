using MediaManager;
using Plugin.Maui.Audio;

namespace Deutschland_Game.Service
{
    public class AudioService
    {
        private bool isPlaying = false;

        public async Task PlayBackgroundAudio()
        {

            Random random = new Random();
            string randNumber = random.Next(1, 4).ToString();
            string audio = $"BackgroundMusic{randNumber}.mp3";
                    
            var player = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audio));
            player.Play();
            player.Loop = true;
        }

        public async void PlayCharacterAudio()
        {
            Random random = new Random();
            string randNumber = random.Next(1, 7).ToString();
            string audio = $"Character0{randNumber}.MP3";
            try
            {
                var player = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audio));
                player.Play();
            }
            catch (Exception ex)
            {
                //tratar erro do audio
            }
        }
        public async void PlayKingAudio(bool decision)
        {

            string dec;
            string extension;
            if (decision) { 
                dec = "Yes";
                extension = "mp3";
            }
            else { 
                dec = "No";
                extension = "MP3";
            }

            Random random = new Random();
            string randNumber = random.Next(1, 4).ToString();
            string audio = $"King{dec}0{randNumber}.{extension}";
            try
            {
                var player = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audio));
                player.Play();
                
            }
            catch (Exception ex)
            {
                //tratar erro do audio
            }
        }
        public async void PlayClickAudio()
        {
            string audio = "ButtonClick.mp3";
            try
            {
                var player = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audio));
                player.Play();
            }
            catch (Exception ex)
            {
                //tratar erro do audio
            }
        }
    }
}

