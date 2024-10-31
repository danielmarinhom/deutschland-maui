using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using Deutschland_Game.Service;


namespace Deutschland_Game.Models.ViewModels
{
    public partial class JogarPageViewModel : ObservableObject
    {
        private readonly ConquistaUsuarioService conquistaUsuarioService;

        [ObservableProperty]
        private ObservableCollection<ConquistaLabelDto> conquistaLabelDtos;

        public JogarPageViewModel()
        {
            conquistaUsuarioService = new ConquistaUsuarioService();

            conquistaLabelDtos = new ObservableCollection<ConquistaLabelDto> {

                new ConquistaLabelDto { text = "test", color = "Red"},
                new ConquistaLabelDto { text = "test", color = "Red"},
                new ConquistaLabelDto { text = "test", color = "Red"},
                new ConquistaLabelDto { text = "test", color = "Red"},
                new ConquistaLabelDto { text = "test", color = "Red"},


            };
        }

        [ObservableProperty]
        private string localImagePath;

        [ObservableProperty]
        private string personagemImagePath;

        [ObservableProperty]
        private string popularidadeText;

        [ObservableProperty]
        private string diplomaciaText;

        [ObservableProperty]
        private string dinheiroText;

        [ObservableProperty]
        private string igrejaText;

        [ObservableProperty]
        private string exercitoText;

        [ObservableProperty]
        private string eraSummary;

        public void setEraPathInImage(string imagePath)
        {
            LocalImagePath = imagePath;
        }

        public void setPersonagemPathInImage(string imagePath)
        {
            PersonagemImagePath = imagePath;
        }

        [RelayCommand]
        public async Task GetAllConquistasByUserID(int userID)
        {

            var response = await conquistaUsuarioService.FindByUserID(userID);

            if (response == null)
            {
                return;
            }

            PopularidadeText = response[0].ValorAcrescentado.ToString();
            IgrejaText = response[1].ValorAcrescentado.ToString();
            DiplomaciaText = response[2].ValorAcrescentado.ToString();
            DinheiroText = response[3].ValorAcrescentado.ToString();
            ExercitoText = response[4].ValorAcrescentado.ToString();

        }

        public async Task<List<int>> SetAdicionalValuesInConquistas(List<ConquistasResponseDto> conquistas, List<Label> labels) //
        {
            List<int> ids= new List<int>();
            foreach (var conquista in conquistas)
            {
                var id = conquista.IdConquista;
                var valor = conquista.ValorAcrescentado;


                if (valor > 0) // se o valor for positivo
                {
                    labels[id - 1].Text = "+" + valor.ToString(); // formatacao para o +50 
                    labels[id - 1].TextColor = Color.FromHex("#7BFA36"); // muda a cor para um verde
                }
                else
                {
                    labels[id - 1].Text = valor.ToString(); // aqui a formatacao já fica no -50
                    labels[id - 1].TextColor = Color.FromHex("#FA3636"); // muda a cor para um vermelho
                }

                ids.Add(id);
            }

            return ids;

        }

        public async Task SetEraNameInSummary(EraResponse eraResponse)
        {
            EraSummary = $"{eraResponse.Nome} {eraResponse.Periodo}";
        }

        public async Task SetSummaryValues(List<Label> labels, List<int> summaryValues)
        {
            for( int i = 0; i < labels.Count; i++)
            {
                var label = labels[i];
                var value = summaryValues[i];

                if (value > 0)
                {
                    label.Text += "+" + value.ToString();
                    label.TextColor = Color.FromHex("#339300");
                }
                else if (value < 0)
                {
                    label.Text += value.ToString();
                    label.TextColor = Color.FromHex("FF0000");
                }
                else
                {
                    label.Text += value.ToString();
                    label.TextColor = Color.FromHex("#FFFFFF");
                }

            }
        }
<<<<<<< HEAD

        
=======
>>>>>>> 14220a0384532f642e27b9b4b1ced62bbbb97f6e
    }
}
