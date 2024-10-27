using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using System.Diagnostics;
using Microsoft.Maui.Storage;
using System.ComponentModel;
using Deutschland_Game.Models.ViewModels;

namespace Deutschland_Game.View;

public partial class JogarPage : ContentPage, INotifyPropertyChanged
{
    private UsuarioDto usuarioDto;
    private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponse;
    private EraResponse eraResponse;
    private JogarPageViewModel viewModel;
    private PersonagemDto personagemDto;

    const string IsFirstLaunchKey = "IsFirstLaunch";

    public JogarPage(UsuarioDto usuarioDto, List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses, EraResponse eraResponse)
    {
        InitializeComponent();

        this.usuarioDto = usuarioDto;
        this.allDatasBeforeEraResponse = allDatasBeforeEraResponses;
        this.eraResponse = eraResponse;

        viewModel = new JogarPageViewModel();
        BindingContext = viewModel;
        LoadEraData();
        LoadPersonagemData();

        tutorialComponent.IsVisible = false;

        if (CheckIfItsFirstTime())
        {
            tutorialComponent.IsVisible = true;
        }

        RunTextStyle();
    }

    public async void LoadEraData()
    {
        await viewModel.DownloadImg64Async(eraResponse.Sprite, eraResponse.Nome);
        OnPropertyChanged(nameof(viewModel.LocalImagePath));
    }
    public async void LoadPersonagemData()
    {
        await viewModel.DownloadPersonagemImg64Async(personagemDto.Sprite, personagemDto.Nome);
        OnPropertyChanged(nameof(viewModel.PersonagemImagePath)); 
    }

    public bool CheckIfItsFirstTime()
    {
        bool itsTheFirstTime = Preferences.Get(IsFirstLaunchKey, true); // verifica se tem essa key no armazenamento local

        if (itsTheFirstTime)
        {
            Preferences.Set(IsFirstLaunchKey, false); // define que o app já foi aberto
            return true;
        }

        return false;
    }

    public async void RunTextStyle()
    {
        await LoadTextStyle("Senhor João", personagemNomeLabel, 100);
        await LoadTextStyle("Senhor, os impostos imperiais são sufocantes e estão causando fome e miséria nas vilas. Precisamos de uma redução nos impostos.", dialogoLabel, 20);
    }

    public async Task LoadTextStyle(string dialog, Label targetLabel, int delayInMls)
    {
        targetLabel.Text = "";
        for (int i = 0; i < dialog.Length; i++)
        {
            targetLabel.Text += dialog[i];
            await Task.Delay(delayInMls);
        }
    }

    private void DialogAccepted(object sender, SwipedEventArgs e)
    {
        if (CheckIfItsFirstTime())
        {
            tutorialComponent.IsVisible = false;
        }

        Debug.WriteLine("ACEITOOO ;)");
    }

    private void DialogRefused(object sender, SwipedEventArgs e)
    {
        if (CheckIfItsFirstTime())
        {
            tutorialComponent.IsVisible = false;
        }

        Debug.WriteLine("RECUSADOOO :(");
    }
}
