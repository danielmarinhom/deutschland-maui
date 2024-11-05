using CommunityToolkit.Maui.Views;
using Deutschland_Game.Service;

namespace Deutschland_Game.View;

public partial class EndPage : ContentPage
{
    private List<int> conquistasValues;

    private bool dialogAlreadyRunning = false;

    private AudioService audioService;

    public EndPage(List<int> conquistasValues, AudioService audioService)
    {
        InitializeComponent();
        this.conquistasValues = conquistasValues;
        PlayEndGame();

        this.audioService = audioService;

        this.audioService.PlayEndGameAudio();
    }

    protected override async void OnAppearing()
    {
        await DialogsAnimation(finalType);
    }

    public async Task LoadTextStyle(string dialog, Label targetLabel, int delayInMls) // animacao de textos 
    {
        targetLabel.Text = "";
        for (int i = 0; i < dialog.Length; i++)
        {
            targetLabel.Text += dialog[i];
            await Task.Delay(delayInMls);
        }
    }



    public async Task DialogsAnimation(Label label)
    {
        var temp = label.Text;
        label.Text = "";

        dialogContainer.Opacity = 1;
        await dialogContainer.TranslateTo(0, -300, 0, Easing.SinInOut);
        dialogContainer.IsVisible = true;
        label.IsVisible = true;

        await dialogContainer.TranslateTo(0, 0, 2000, Easing.SinInOut);

        if (label == finalType)
        {
            await LoadTextStyle(temp, label, 100);

        }
        else
        {
            await LoadTextStyle(temp, label, 20);
        }

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
                finalType.Text = "Final Popular!";
                EndText.Text = "Durante o seu reinado, o povo prosperou, e a Alemanha se ergueu como uma na��o de abund�ncia e alegria, onde a felicidade e o bem-estar prevaleceram.";
                break;
            case 1: // igreja
                EndVideo.Source = MediaSource.FromResource("igreja.mp4");
                EndText.Text = " Durante o seu reinado, a f� foi o alicerce do imp�rio, unindo a Alemanha sob uma devo��o profunda, guiada pelos princ�pios da igreja.";
                break;
            case 2: // diplomacia
                EndVideo.Source = MediaSource.FromResource("diplomacia.mp4");
                finalType.Text = "Final Diplom�tico!";
                EndText.Text = "Durante o seu reinado, a Alemanha p�de firmar alian�as poderosas, garantindo paz e prosperidade duradouras atrav�s da diplomacia.";
                break;
            case 3: // economia
                EndVideo.Source = MediaSource.FromResource("economia.mp4");
                finalType.Text = "Final Econ�mico!";
                EndText.Text = "Durante o seu reinado, a economia da Alemanha floresceu, acumulando riquezas imensur�veis que fortaleceram o reino e garantiram sua estabilidade financeira.";
                break;
            case 4: // exercito
                EndVideo.Source = MediaSource.FromResource("exercito.mp4");
                finalType.Text = "Final Guerreiro!";
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
        await audioService.StopBackGroundAudio();
        await Navigation.PopToRootAsync();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (dialogAlreadyRunning)
        {
            return;
        }
        dialogAlreadyRunning = true;
        await dialogContainer.TranslateTo(0, -300, 2000, Easing.SinInOut);
        finalType.Text = "";
        await DialogsAnimation(EndText);

        btnComponent.Opacity = 0;
        btnComponent.IsVisible = true;
        await btnComponent.FadeTo(1, 1000, Easing.SinInOut);
    }
}
