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
                EndText.Text = "Durante o seu reinado, o povo prosperou, e a Alemanha se ergueu como uma nação de abundância e alegria, onde a felicidade e o bem-estar prevaleceram.";
                break;
            case 1: // igreja
                EndVideo.Source = MediaSource.FromResource("igreja.mp4");
                EndText.Text = " Durante o seu reinado, a fé foi o alicerce do império, unindo a Alemanha sob uma devoção profunda, guiada pelos princípios da igreja.";
                break;
            case 2: // diplomacia
                EndVideo.Source = MediaSource.FromResource("diplomacia.mp4");
                finalType.Text = "Final Diplomático!";
                EndText.Text = "Durante o seu reinado, a Alemanha pôde firmar alianças poderosas, garantindo paz e prosperidade duradouras através da diplomacia.";
                break;
            case 3: // economia
                EndVideo.Source = MediaSource.FromResource("economia.mp4");
                finalType.Text = "Final Econômico!";
                EndText.Text = "Durante o seu reinado, a economia da Alemanha floresceu, acumulando riquezas imensuráveis que fortaleceram o reino e garantiram sua estabilidade financeira.";
                break;
            case 4: // exercito
                EndVideo.Source = MediaSource.FromResource("exercito.mp4");
                finalType.Text = "Final Guerreiro!";
                EndText.Text = "Durante o seu reinado, o poderio militar da Alemanha foi incontestável, conquistando glórias e garantindo segurança a todos os cantos do império.";
                break;
            default:
                EndText.Text = "Nenhuma categoria foi dominante no seu reinado.";
                break;
        }

        EndVideo.Play();
        await Task.Delay(6000);
        EndVideo.Pause();// pausando pq o vídeo termina com um fade opacity
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
