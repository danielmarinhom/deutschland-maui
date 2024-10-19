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
<<<<<<< HEAD

        private async void jogarBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EscolherNomePage());
        }
=======
>>>>>>> a6e09fecbdb0c67935aa73cc8aeb02177bfa0a10
    }

}
