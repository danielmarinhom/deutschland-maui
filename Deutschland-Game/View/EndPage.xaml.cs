namespace Deutschland_Game.View;

public partial class EndPage : ContentPage
{
    private List<int> conquistasValues;

    public EndPage(List<int> conquistasValues)
    {
        InitializeComponent();
        this.conquistasValues = conquistasValues;
        PlayEndGame();
    }

    private void PlayEndGame()
    {
        int maiorValor = conquistasValues.Max();
        int index = -1;
        for (int i = 0; i < conquistasValues.Count; i++) {
            if (conquistasValues[i] == maiorValor) { index = i; break; }
        }
        switch (index)
        {
            case 0: // popularidade
                EndVideo.Source = "resource://Deutschland_Game.Resources.Videos.popularidade.mp4";
                EndText.Text = "Durante o seu reinado, o povo prosperou, e a Alemanha se ergueu como uma nação de abundância e alegria, onde a felicidade e o bem-estar prevaleceram.";
                break;
            case 1: // igreja
                EndVideo.Source = "resource://Deutschland_Game.Resources.Videos.igreja.mp4";
                EndText.Text = " Durante o seu reinado, a fé foi o alicerce do império, unindo a Alemanha sob uma devoção profunda, guiada pelos princípios da igreja.";
                break;
            case 2: // diplomacia
                EndVideo.Source = "resource://Deutschland_Game.Resources.Videos.diplomacia.mp4";
                EndText.Text = "Durante o seu reinado, a Alemanha pôde firmar alianças poderosas, garantindo paz e prosperidade duradouras através da diplomacia.";
                break;
            case 3: // economia
                EndVideo.Source = "resource://Deutschland_Game.Resources.Videos.economia.mp4";
                EndText.Text = "Durante o seu reinado, a economia da Alemanha floresceu, acumulando riquezas imensuráveis que fortaleceram o reino e garantiram sua estabilidade financeira.";
                break;
            case 4: // exercito
                EndVideo.Source = "resource://Deutschland_Game.Resources.Videos.exercito.mp4";
                EndText.Text = "Durante o seu reinado, o poderio militar da Alemanha foi incontestável, conquistando glórias e garantindo segurança a todos os cantos do império.";
                break;
            default:
                EndText.Text = "Nenhuma categoria foi dominante no seu reinado.";
                break;
        }
    }
    private async void OnMainMenuButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

}
