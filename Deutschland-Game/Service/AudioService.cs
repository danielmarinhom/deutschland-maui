using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;

namespace Deutschland_Game.Service
{
    public class AudioService
    {
        private bool isPlaying = false;

        public async Task PlayBackgroundAudio()
        {
            if (!isPlaying)
            {
                try
                {
                    Random random = new Random();
                    string randNumber = random.Next(1, 4).ToString();
                    string audio = $"BackgroundMusic{randNumber}.mp3";
                    string audioFilePath = $"resource://Deutschland_Game.Resources.Audios.BackgroundMusics.{audio}";

                    CrossMediaManager.Current.RepeatMode = RepeatType.One;

                    await CrossMediaManager.Current.Play(audioFilePath, MediaFileType.Audio);
                    isPlaying = true;
                }
                catch (Exception ex)
                {
                    // tratar erro do audio
                }
            }
        }

        public async Task StopAudio()
        {
            if (isPlaying)
            {
                await CrossMediaManager.Current.Stop();
                isPlaying = false;
            }
        }

        public async void PlayCharacterAudio()
        {
            Random random = new Random();
            string randNumber = random.Next(1, 7).ToString();
            string audio = $"Character0{randNumber}.mp3";
            try
            {
                string audioFilePath = $"resource://Deutschland_Game.Resources.Audios.SpeakSounds.{audio}";
                await CrossMediaManager.Current.Play(audioFilePath, MediaFileType.Audio);
            }
            catch (Exception ex)
            {
                //tratar erro do audio
            }
        }
        public async void PlayKingAudio(bool decision)
        {

            string dec;

            if (decision) { dec = "Yes"; }
            else { dec = "No"; }

            Random random = new Random();
            string randNumber = random.Next(1, 4).ToString();
            string audio = $"King{dec}0{randNumber}.mp3";
            try
            {
                string audioFilePath = $"resource://Deutschland_Game.Resources.Audios.SpeakSounds.{audio}";
                await CrossMediaManager.Current.Play(audioFilePath, MediaFileType.Audio);
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
                string audioFilePath = $"resource://Deutschland_Game.Resources.Audios.GameEffects.{audio}";
                await CrossMediaManager.Current.Play(audioFilePath, MediaFileType.Audio);
            }
            catch (Exception ex)
            {
                //tratar erro do audio
            }
        }
    }
}

