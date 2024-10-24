using Deutschland_Game.Service;
using Deutschland_Game.View;
using System.Diagnostics;
using System.Text.Json;

namespace Deutschland_Game
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

        }

        private async void creditosBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreditosPage());
        }

        private async void jogarBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EscolherNomePage());
        }
    }

}
