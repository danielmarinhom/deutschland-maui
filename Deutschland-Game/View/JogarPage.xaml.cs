using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using System.Diagnostics;
using System.ComponentModel;
using Deutschland_Game.Models.ViewModels;
using Deutschland_Game.Service;
using Microsoft.Maui.Controls.PlatformConfiguration;


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

    private bool userPassedAnimation = false;

    private ConquistaUsuarioService conquistaUsuarioService;

    private List<Label> conquistasLabels;
    private List<int> conquistasValues;

    public JogarPage(UsuarioDto usuarioDto, List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses, string eraImagePath, List<string> personagemImagesPaths)

    {
        InitializeComponent();

        conquistasLabels = new List<Label> { populaidadeAdicional, igrejaAdicional, diplomaciaAdicional, economiaAdicional, exercitoAdicional };

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
    public void AtualizarConquistas(bool wasAceppt) // atualiza a lista global das conquistas a cada escolha
    {
        foreach (var dialogo in allDatasBeforeEraResponse)
        {
            List<ConquistasResponseDto> conquistas;

            if (wasAceppt)
            {
                conquistas = dialogo.Consequencias.aceito;
            }
            else
            {
                conquistas = dialogo.Consequencias.recusado;
            }

            foreach (var conquista in conquistas)
            {
                conquistasValues[conquista.IdConquista - 1] += (int)conquista.ValorAcrescentado;
            }
        }
    }
    public List<int> endGame() // no fim do jogo retorna os valores acumulados ate o fim da era
    {
        return conquistasValues;
    }


    public async void AdicionalAnimation(Label label)
    {
        label.IsVisible = true;

        label.Opacity = 0;

        label.FadeTo(1, 500, Easing.SinOut);

        await label.TranslateTo(0, -20, 800, Easing.BounceIn);

        await label.FadeTo(0, 500, Easing.SinOut);

        label.IsVisible = false;

        await label.TranslateTo(0, 0, 10, Easing.Linear);

    }

    public string TempFixDialogsContents(string content) // metodo tempor�rio por conta dos dialogos que foram inseridos com quebra linha no banco
    {
        return content.Replace("\r\n", " ");
    }

    public async void CharacterJoinInScene()
    {
        var translate = personagemComponent.TranslateTo(-80, 0, 2000, Easing.SinInOut);

        await Task.WhenAll(translate);
    }

    public async void CharacterLeaveInScene()
    {
        var translate = personagemComponent.TranslateTo(0, 0, 2000, Easing.SinInOut);

        await Task.WhenAll(translate);
    }

    public async void ShowChoiceEffect(bool wasAccept)
    {
        
        if(wasAccept)
        {

        }

    } // fazer dps

    public bool CheckIfItsFirstTime()
	{

		bool itsTheFirstTime = Preferences.Get(IsFirstLaunchKey, true); // verifica se tem essa key no armazenamento local

		if (itsTheFirstTime)
		{

			Preferences.Set(IsFirstLaunchKey, false); // define q o app j� foi aberto
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
            if (userPassedAnimation)
            {
                targetLabel.Text = dialog;
                userPassedAnimation = false;
                return;
            }
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

    public async void ChoiceMade(bool wasAceppt)
    {

        if (actIndexDialog >= allDatasBeforeEraResponse.Count)
        {
            return;
        }
        AtualizarConquistas(wasAceppt);
        List<ConquistasResponseDto> conquistas = new List<ConquistasResponseDto>();
        if (wasAceppt)
        {
            conquistas = allDatasBeforeEraResponse[actIndexDialog].Consequencias.aceito;
        }
        else
        {
            conquistas = allDatasBeforeEraResponse[actIndexDialog].Consequencias.recusado;
        }

        var ids = await viewModel.SetAdicionalValuesInConquistas(conquistas, conquistasLabels);

        for (int i = 0; i < ids.Count; i++)
        {
            AdicionalAnimation(conquistasLabels[ids[i] - 1]);
        }

        await conquistaUsuarioService.UpdateConquistas(conquistas, usuarioDto.Id);

        Vibration.Vibrate(200);

        ShowChoiceEffect(wasAceppt);
        await Task.Delay(300);
        ShowChoiceEffect(wasAceppt);

        await RunAnswer(actIndexDialog, wasAceppt);

        actIndexDialog++;

        if(actIndexDialog >= allDatasBeforeEraResponse.Count)
        {
            return;
        }

        await Task.Delay(1500);

        await RunDialog(actIndexDialog);
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

        ChoiceMade(true);
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

        ChoiceMade(false);
    }

    private void PassDialog(object sender, EventArgs e) // m�todo para pular a anima��o do di�logo 
    {
        userPassedAnimation = true;
    }
}
