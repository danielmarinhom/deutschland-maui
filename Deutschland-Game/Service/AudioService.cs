using MediaManager;
using Plugin.Maui.Audio;
using System.Diagnostics;
using AudioManager = Plugin.Maui.Audio.AudioManager;

namespace Deutschland_Game.Service
{
    public class AudioService
    {
        private bool isPlaying = false;

        private readonly IAudioManager audioManager;

        private IAudioPlayer audioPlayer;

        public AudioService(IAudioManager audio)
        {
            audioManager = audio;
        }

        public async Task PlayBackgroundAudio()
        {

            Random random = new Random();
            string randNumber = random.Next(1, 4).ToString();
            string audio = $"BackgroundMusic{randNumber}.mp3";

            this.audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audio));

            this.audioPlayer.Loop = true;
            this.audioPlayer.Play();

        }

        public async Task StopBackGroundAudio()
        {
            this.audioPlayer.Stop();
        }

        public async void PlayCharacterAudio()
        {
            Random random = new Random();
            string randNumber = random.Next(1, 7).ToString();
            string audio = $"Character0{randNumber}.MP3";
            try
            {
                var player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audio));
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
                var player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audio));
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
                var player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audio));
                player.Play();
            }
            catch (Exception ex)
            {
                //tratar erro do audio
            }
        }
    }
}

