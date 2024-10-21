using Deutschland_Game.View;

namespace Deutschland_Game
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();



            MainPage = new AppShell();
            //MainPage = new GamePage();

        }
    }
}
