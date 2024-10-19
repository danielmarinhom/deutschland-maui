using Deutschland_Game.View;

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
