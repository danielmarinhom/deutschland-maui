using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Deutschland_Game.View
{
    public partial class LoadingPage : ContentPage
    {
        //puxar id do cadastro da api e passar no JogarPage no parametro do Navigation
        private double id;
        public LoadingPage()
        {
            InitializeComponent();
            SimulateLoading();
        }

        private async void SimulateLoading()
        {
            for (double progress = 0; progress <= 1; progress += 0.1)
            {
                progressBar.Progress = progress;
                progressLabel.Text = $"{(int)(progress * 100)}%";
                await Task.Delay(500);
            }

            await Navigation.PushAsync(new JogarPage(id));
        }
        /*
         to-do:
            fazer o service para puxar o id passando o nome do usuario, se deu certo ->
            criar um model em c# dos dialogos da era
            mandar o id pra tela de loading
            mandar o model pra tela de jogar a partir do loading
         */
    }

}
