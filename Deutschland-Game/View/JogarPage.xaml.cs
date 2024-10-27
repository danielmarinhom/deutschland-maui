using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using System.Diagnostics;
using System.ComponentModel;
using Deutschland_Game.Models.ViewModels;


namespace Deutschland_Game.View;

public partial class JogarPage : ContentPage, INotifyPropertyChanged
{

    private UsuarioDto usuarioDto;

    private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponse;

    private JogarPageViewModel viewModel;

    private List<string> personagemImagesPaths;

    const string IsFirstLaunchKey = "IsFirstLaunch";

	private int actIndexDialog = 0;

    public JogarPage(UsuarioDto usuarioDto, List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses, string eraImagePath, List<string> personagemImagesPaths)

    {
        InitializeComponent();

        viewModel = new JogarPageViewModel();
        BindingContext = viewModel;
        viewModel.setEraPathInImage(eraImagePath);

        this.usuarioDto = usuarioDto;
        this.allDatasBeforeEraResponse = allDatasBeforeEraResponses;
        
        this.personagemImagesPaths = personagemImagesPaths;

        RunDialog(0);
        
        tutorialComponent.IsVisible = false;

        if (CheckIfItsFirstTime())
        {
            tutorialComponent.IsVisible = true;
        }



    }

    public bool CheckIfItsFirstTime()
	{

		bool itsTheFirstTime = Preferences.Get(IsFirstLaunchKey, true); // verifica se tem essa key no armazenamento local

		if (itsTheFirstTime)
		{

			Preferences.Set(IsFirstLaunchKey, false); // define q o app já foi aberto
			return true;

        }

		return false;

    }

	public async Task RunTextStyle(string characterName, string dialogContent)
	{
        dialogoContainer.IsVisible = true;
        await LoadTextStyle(characterName, personagemNomeLabel, 100);
        await LoadTextStyle(dialogContent, dialogoLabel, 20);
    }

    public async Task LoadTextStyle(string dialog, Label targetLabel, int delayInMls)
	{
		targetLabel.Text = "";
		for(int i = 0; i < dialog.Length; i++)
		{
			targetLabel.Text += dialog[i];
			await Task.Delay(delayInMls);
		}
	}

	public async Task RunDialog(int index)
	{

		string characterName = allDatasBeforeEraResponse[index].Personagem.Nome;
		string dialogContent = allDatasBeforeEraResponse[index].mensagem;

		await RunTextStyle(characterName, dialogContent);

	}

    public async Task RunAnswer(int index, bool wasAccpted)
    {

        string username = usuarioDto.Nome;

        string dialogContent;


        if (wasAccpted)
        {
            dialogContent = allDatasBeforeEraResponse[index].Respostas.Aceito;
            return;
        }

        dialogContent = allDatasBeforeEraResponse[index].Respostas.Recusado;

        RunTextStyle(username, dialogContent);

    }

    private async void DialogAccepted(object sender, SwipedEventArgs e)
    {

		if (CheckIfItsFirstTime())
		{
            tutorialComponent.IsVisible = false;
        }

        await RunAnswer(actIndexDialog, true);
        actIndexDialog++;
        await Task.Delay(500);

        await RunDialog(actIndexDialog);
    }

    private async void DialogRefused(object sender, SwipedEventArgs e)
    {

        if (CheckIfItsFirstTime())
        {
            tutorialComponent.IsVisible = false;
        }

        await RunAnswer(actIndexDialog, false);
        actIndexDialog++;
        await Task.Delay(500);

        await RunDialog(actIndexDialog);

        Debug.WriteLine("RECUSADOOO :(");

    }
}
