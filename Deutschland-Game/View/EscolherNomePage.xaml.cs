using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Deutschland_Game.View
{
    public partial class EscolherNomePage : ContentPage, INotifyPropertyChanged
    {
        private string nomeRei;
        public string NomeRei
        {
            get => nomeRei;
            set{
                if (nomeRei != value){ nomeRei = value;  OnPropertyChanged(); }
            }
        }

        public EscolherNomePage()
        {
            InitializeComponent();
            BindingContext = this;
            NomeRei = "Francisco Barbarossa";
        }
        // update left scroll according to right entry irt
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void continuarBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoadingPage());
        }
    }
}