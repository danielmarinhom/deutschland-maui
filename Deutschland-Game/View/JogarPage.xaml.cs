using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using System.Diagnostics;
using System.ComponentModel;
using Deutschland_Game.Models.ViewModels;
using System.Net.Mime;
using Deutschland_Game.Service;


namespace Deutschland_Game.View;

public partial class JogarPage : ContentPage, INotifyPropertyChanged
{

    private UsuarioDto usuarioDto;

    private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponse;

    private JogarPageViewModel viewModel;

    private List<string> personagemImagesPaths;

    const string IsFirstLaunchKey = "IsFirstLaunch";

	private int actIndexDialog = 0;

    private bool isTaskingRunning = false;

    private ConquistaUsuarioService conquistaUsuarioService;

    public JogarPage(UsuarioDto usuarioDto, List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses, string eraImagePath, List<string> personagemImagesPaths)

    {
        InitializeComponent();

        viewModel = new JogarPageViewModel();
        BindingContext = viewModel;
        viewModel.setEraPathInImage(eraImagePath);

        viewModel.GetAllConquistasByUserID(usuarioDto.Id);

        this.conquistaUsuarioService = new ConquistaUsuarioService();

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

    public string TempFixDialogsContents(string content) // metodo temporário por conta dos dialogos que foram inseridos com quebra linha no banco
    {
        return content.Replace("\r\n", " ");
    }

    public async void CharacterJoinInScene()
    {
        Debug.WriteLine(personagemComponent.Y);
        var translate = personagemComponent.TranslateTo(-80, 0, 2000, Easing.SinInOut);

        await Task.WhenAll(translate);
    }

    public async void CharacterLeaveInScene()
    {
        Debug.WriteLine(personagemComponent.X);
        var translate = personagemComponent.TranslateTo(0, 0, 2000, Easing.SinInOut);

        await Task.WhenAll(translate);
    }

    public async void ShowChoiceEffect(bool wasAccept)
    {
        if (wasAccept)
        {
            choiceEffect.BackgroundColor = Color.FromHex("#00c1a2");
        }
        else
        {
            choiceEffect.BackgroundColor = Color.FromHex("#de524b");
        }
        choiceEffect.IsVisible = true;
        await Task.Delay(300);
        choiceEffect.IsVisible = false;

    }

    public bool CheckIfItsFirstTime()
	{

		bool itsTheFirstTime = Preferences.Get(IsFirstLaunchKey, true); // verifica se tem essa key no armazenamento local

		if (itsTheFirstTime)
		{

			Preferences.Set(IsFirstLaunchKey, false); // define q o app já foi aberto
			return true;

        }

		return true; // colocar false para a versao final

    }

	public async Task RunTextStyle(string characterName, string dialogContent)
	{

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
        isTaskingRunning = true;
        CharacterJoinInScene();

        personagemNomeLabel.Text = "";
        dialogoLabel.Text = "";
        dialogoContainer.IsVisible = true;

        string characterName = allDatasBeforeEraResponse[index].Personagem.Nome;
		string dialogContent = allDatasBeforeEraResponse[index].mensagem;

        viewModel.setPersonagemPathInImage(personagemImagesPaths[index]);
		await RunTextStyle(characterName, TempFixDialogsContents(dialogContent));
        isTaskingRunning = false;
    }

    public async Task RunAnswer(int index, bool wasAccpted)
    {
        isTaskingRunning = true;

        await viewModel.GetAllConquistasByUserID(usuarioDto.Id);

        personagemNomeLabel.Text = "";
        dialogoLabel.Text = "";

        string username = usuarioDto.Nome + " I";

        string dialogContent;

        if (wasAccpted)
        {
            dialogContent = allDatasBeforeEraResponse[index].Respostas.Aceito;
        }
        else
        {
            dialogContent = allDatasBeforeEraResponse[index].Respostas.Recusado;
        }


        await RunTextStyle(username, TempFixDialogsContents(dialogContent));

        await Task.Delay(500);

        dialogoContainer.IsVisible = false;

        CharacterLeaveInScene();

        isTaskingRunning = false;


    }

    private async void DialogAccepted(object sender, SwipedEventArgs e)
    {

        if (isTaskingRunning)
        {
            return;
        }

		if (CheckIfItsFirstTime())
		{
            tutorialComponent.IsVisible = false;
        }

        await conquistaUsuarioService.UpdateConquistas(allDatasBeforeEraResponse[actIndexDialog].Consequencias.aceito, usuarioDto.Id);

        Vibration.Vibrate(200);

        ShowChoiceEffect(true);
        await Task.Delay(300);
        ShowChoiceEffect(true);

        await RunAnswer(actIndexDialog, true);

        actIndexDialog++;

        await Task.Delay(1500);

        await RunDialog(actIndexDialog);
    }

    private async void DialogRefused(object sender, SwipedEventArgs e)
    {

        if (isTaskingRunning)
        {
            return;
        }

        if (CheckIfItsFirstTime())
        {
            tutorialComponent.IsVisible = false;
        }

        await conquistaUsuarioService.UpdateConquistas(allDatasBeforeEraResponse[actIndexDialog].Consequencias.recusado, usuarioDto.Id);

        Vibration.Vibrate(200);

        ShowChoiceEffect(false);
        await Task.Delay(300);
        ShowChoiceEffect(false);

        await RunAnswer(actIndexDialog, false);

        actIndexDialog++;

        await Task.Delay(1500);

        await RunDialog(actIndexDialog);

    }
}
