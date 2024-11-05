using CommunityToolkit.Maui.Views;

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

    private async void PlayEndGame()
    {
        int maiorValor = conquistasValues.Max();
        int index = -1;
        for (int i = 0; i < conquistasValues.Count; i++) {
            if (conquistasValues[i] == maiorValor) { index = i; break; }
        }

        switch (index)
        {
            case 0: // popularidade
                EndVideo.Source = MediaSource.FromResource("popularidade.mp4");
                EndText.Text = "Durante o seu reinado, o povo prosperou, e a Alemanha se ergueu como uma na��o de abund�ncia e alegria, onde a felicidade e o bem-estar prevaleceram.";
                break;
            case 1: // igreja
                EndVideo.Source = MediaSource.FromResource("igreja.mp4");
                EndText.Text = " Durante o seu reinado, a f� foi o alicerce do imp�rio, unindo a Alemanha sob uma devo��o profunda, guiada pelos princ�pios da igreja.";
                break;
            case 2: // diplomacia
                EndVideo.Source = MediaSource.FromResource("diplomacia.mp4");
                EndText.Text = "Durante o seu reinado, a Alemanha p�de firmar alian�as poderosas, garantindo paz e prosperidade duradouras atrav�s da diplomacia.";
                break;
            case 3: // economia
                EndVideo.Source = MediaSource.FromResource("economia.mp4");
                EndText.Text = "Durante o seu reinado, a economia da Alemanha floresceu, acumulando riquezas imensur�veis que fortaleceram o reino e garantiram sua estabilidade financeira.";
                break;
            case 4: // exercito
                EndVideo.Source = MediaSource.FromResource("exercito.mp4");
                EndText.Text = "Durante o seu reinado, o poderio militar da Alemanha foi incontest�vel, conquistando gl�rias e garantindo seguran�a a todos os cantos do imp�rio.";
                break;
            default:
                EndText.Text = "Nenhuma categoria foi dominante no seu reinado.";
                break;
        }

        EndVideo.Play();
        await Task.Delay(6000);
        EndVideo.Pause();// pausando pq o v�deo termina com um fade opacity
    }
    private async void OnMainMenuButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

}
