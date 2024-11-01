using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using System.ComponentModel;
using Deutschland_Game.Models.ViewModels;
using Deutschland_Game.Service;
using System.Diagnostics;


namespace Deutschland_Game.View;

public partial class JogarPage : ContentPage, INotifyPropertyChanged
{

    private UsuarioDto usuarioDto;

    private EraService eraService = new EraService();

    private readonly AudioService audioService;

    private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponse;

    private JogarPageViewModel viewModel;

    private List<string> personagemImagesPaths;

    const string IsFirstLaunchKey = "IsFirstLaunch";

	private int actIndexDialog = 0;

    private bool isTaskingRunning = false;

    private bool userPassedAnimation = false;

    private bool allDialogsAnswered = false;

    private ConquistaUsuarioService conquistaUsuarioService;

    private List<Label> conquistasLabels;

    private List<Label> summaryLabels;

    private EraResponse eraResponseGlobal;

    private List<int> conquistasValues;

    private UsuarioService usuarioService;

    public JogarPage(UsuarioDto usuarioDto, List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses, string eraImagePath, List<string> personagemImagesPaths, EraResponse eraResponse, AudioService audioService)

    {
        InitializeComponent();

        this.audioService = audioService;

        this.usuarioService = new UsuarioService();

        conquistasLabels = new List<Label> { populaidadeAdicional, igrejaAdicional, diplomaciaAdicional, economiaAdicional, exercitoAdicional };

        summaryLabels = new List<Label> { popularidadeSum, igrejaSum, diplomaciaSum, economiaSum, exercitoSum };

        conquistasValues = new List<int> { 0, 0, 0, 0, 0};

        viewModel = new JogarPageViewModel();
        BindingContext = viewModel;
        viewModel.setEraPathInImage(eraImagePath);

        viewModel.GetAllConquistasByUserID(usuarioDto.Id);

        this.conquistaUsuarioService = new ConquistaUsuarioService();

        this.eraResponseGlobal = eraResponse;

        this.usuarioDto = usuarioDto;
        this.allDatasBeforeEraResponse = allDatasBeforeEraResponses;
        
        this.personagemImagesPaths = personagemImagesPaths;

        this.audioService.PlayBackgroundAudio();

        RunDialog(0);
        
        tutorialComponent.IsVisible = false;

        if (CheckIfItsFirstTime())
        {
            tutorialComponent.IsVisible = true;
        }

    }
    public async Task AtualizarConquistas(bool wasAceppt) // atualiza a lista global das conquistas a cada escolha
    {
        audioService.PlayKingAudio(wasAceppt);

        List<ConquistasResponseDto> conquistas = new List<ConquistasResponseDto>(); // tive que mudar essa lógica aqui, Daniel
            //antes ele tava somando todas as consequencias de todos os dialogos, pq vc tava fazendo um for no allDatasBeforeEra
            // agora ele só pega o dialogo de acordo com o index global da classe, e filtra certinho

        if (wasAceppt)
            {
                conquistas = allDatasBeforeEraResponse[actIndexDialog].Consequencias.aceito;
            }
            else
            {
                conquistas = allDatasBeforeEraResponse[actIndexDialog].Consequencias.recusado;
            }

            // aqui eu só joguei esses códigos para dentro desse método, antes tava no ChoiceMade()
            await conquistaUsuarioService.UpdateConquistas(conquistas, usuarioDto.Id); // faz o update das conquistasUsuario pela api 

            var ids = await viewModel.SetAdicionalValuesInConquistas(conquistas, conquistasLabels);

            for (int i = 0; i < ids.Count; i++) // pra rodar as animações de +50, -100 e etc
            {
                AdicionalAnimation(conquistasLabels[ids[i] - 1]);
            }

            foreach (var conquista in conquistas)
            {
                conquistasValues[conquista.IdConquista - 1] += (int) conquista.ValorAcrescentado;
            }
    }

    public async Task<bool> ValidateGameOver() // animacoes para a tela de game over
    {

        bool isGameOver = await viewModel.isGameOver(this.usuarioDto.Id); // valida se tem alguma conquistaUsuario negativa

        if (isGameOver) { // animacoes

            await usuarioService.deleteUser(this.usuarioDto.Id); // deleta o usuario atual

            loading_component.IsVisible = false;

            gameOverTransition.Opacity = 0;

            gameOverTransition.IsVisible = true;
            await gameOverTransition.FadeTo(1, 4000, Easing.SinInOut);

            gameContainer.IsVisible = false;
            gameOverContainer.IsVisible = true;

            await gameOverTransition.FadeTo(0, 4000, Easing.SinInOut);


            return true;
        }

        return false;

    }

    public async void RunEndEraAnimation()  // animacoes do Summary da Era
    {
        conffetsAnimation.IsEnabled = false;
        bkgSummaryContainer.Opacity = 0;

        await bkgSummaryContainer.FadeTo(1, 1000, Easing.SinInOut);

        conffetsAnimation.RepeatCount = 0;
        conffetsAnimation.IsEnabled = true;

        await summaryInfoContainer.TranslateTo(0, 0, 1000, Easing.SinInOut);

        summaryButton.IsEnabled = true;
    }

    public async void ShowEndGameDialog() // valida se é game over ou pode passar para a próxima era
    {

        loading_component.IsVisible = true;

        bool runGameOver = await ValidateGameOver();
        Debug.WriteLine(runGameOver);
        if (runGameOver) // se for game over, vai cancelar as animacoes de Summary
        {
            return;
        }

        // animacoes para o Summary da Era
        await summaryInfoContainer.TranslateTo(0, -500, 10);
        await viewModel.SetEraNameInSummary(eraResponseGlobal);
        await viewModel.SetSummaryValues(summaryLabels, conquistasValues);
        summaryContainer.IsVisible = true;

        loading_component.IsVisible = false;

        RunEndEraAnimation();

    }

    public async void AdicionalAnimation(Label label) // animacao dos valores acrescentados nas conquistas
    {
        label.IsVisible = true;

        label.Opacity = 0;

        label.FadeTo(1, 500, Easing.SinOut); // opacidade do 0 para 1

        await label.TranslateTo(0, -20, 800, Easing.BounceIn);

        await label.FadeTo(0, 500, Easing.SinOut);

        label.IsVisible = false;

        await label.TranslateTo(0, 0, 10, Easing.Linear);

    }

    public string TempFixDialogsContents(string content) // metodo temporário por conta dos dialogos que foram inseridos com quebra linha no banco
    {
        return content.Replace("\r\n", " ");
    }

    public async void CharacterJoinInScene() // animacao de entrada do personagem
    {
        var translate = personagemComponent.TranslateTo(-80, 0, 2000, Easing.SinInOut);
        audioService.PlayCharacterAudio();
        await Task.WhenAll(translate);
    }

    public async void CharacterLeaveInScene() // animacao de saída do personagem
    {
        var translate = personagemComponent.TranslateTo(0, 0, 2000, Easing.SinInOut);

        await Task.WhenAll(translate);
    }

    public bool CheckIfItsFirstTime()
	{

		bool itsTheFirstTime = Preferences.Get(IsFirstLaunchKey, true); // verifica se tem essa key no armazenamento local

        Debug.WriteLine(itsTheFirstTime);

		if (itsTheFirstTime)
		{

			Preferences.Set(IsFirstLaunchKey, false); // define q o app já foi aberto
			return true;

        }

		return false; // colocar false para a versao final

    }

    public async Task LoadTextStyle(string dialog, Label targetLabel, int delayInMls) // animacao de textos 
    {
        targetLabel.Text = "";
        for (int i = 0; i < dialog.Length; i++)
        {
            if (userPassedAnimation) // valida se o usuario clicar na tela, ele passa a animacao
            {
                targetLabel.Text = dialog;
                userPassedAnimation = false;
                return;
            }
            targetLabel.Text += dialog[i];
            await Task.Delay(delayInMls);
        }
    }

    public async Task RunTextStyle(string characterName, string dialogContent)
	{
        // executa em ordem - nome do personagem e depois o conteudo
        await LoadTextStyle(characterName, personagemNomeLabel, 100); // nome do personagem
        await LoadTextStyle(dialogContent, dialogoLabel, 20); // conteudo do dialogo
    }

	public async Task RunDialog(int index) // organiza os dados da Response para rodar o Dialogo
	{
        isTaskingRunning = true;
        
        viewModel.setPersonagemPathInImage(personagemImagesPaths[index]); // seta o path da imagem para o Xaml
        CharacterJoinInScene(); // animacao de entrada do personagem

        personagemNomeLabel.Text = ""; // define pra garantir que vai carregar do zero
        dialogoLabel.Text = "";

        dialogoContainer.IsVisible = true; // deixa visível a imagem da folha do dialogo

        string characterName = allDatasBeforeEraResponse[index].Personagem.Nome; // pega o nome do personagem
		string dialogContent = allDatasBeforeEraResponse[index].mensagem; // pega o conteudo do dialogo

		await RunTextStyle(characterName, TempFixDialogsContents(dialogContent)); // roda a animacao do dialoog e do nome do personagem

        isTaskingRunning = false; // habilita o usuario a escolher a proposta

    }

    public async Task RunAnswer(int index, bool wasAccpted) // organiza os dados da resposta do rei
    {
        isTaskingRunning = true; // usuario nao pode mais fazer interação

        await viewModel.GetAllConquistasByUserID(usuarioDto.Id); // atualiza a conquista do usuario fazendo requesting na api

        personagemNomeLabel.Text = "";
        dialogoLabel.Text = "";

        string username = usuarioDto.Nome + " I";

        string dialogContent;

        if (wasAccpted) // filtra os dados de acordo com a escolha do usuario
        {
            dialogContent = allDatasBeforeEraResponse[index].Respostas.Aceito;
        }
        else
        {
            dialogContent = allDatasBeforeEraResponse[index].Respostas.Recusado;
        }


        await RunTextStyle(username, TempFixDialogsContents(dialogContent)); // roda as animacoes dos dialogos

        await Task.Delay(500); // delay só pra decorar o jogo

        dialogoContainer.IsVisible = false; // esconde a imagem da folha 

        CharacterLeaveInScene(); // animacao do personagem saindo de cena

    }

    public async void ChoiceMade(bool wasAceppt) // wasAceppt = se o dialogo foi aceito
    {

        await AtualizarConquistas(wasAceppt);

        Vibration.Vibrate(200); // vibração do celular pra decoração do jogo

        await RunAnswer(actIndexDialog, wasAceppt); // aqui roda o dialogo do rei como resposta

        actIndexDialog++; // aumenta o indice pra indicar o proximo dialogo a ser carregado

        if(actIndexDialog >= allDatasBeforeEraResponse.Count)
        {
            // se o indice do dialogo for >= ao tamanha da lista ---- é pra nao dar exception de index no allDatasBeforeEraResponse
            // basicamente significa que os dialogos já foram todos carregados
            allDialogsAnswered = true;

            ShowEndGameDialog();

            return;
        }

        await Task.Delay(1500);

        await RunDialog(actIndexDialog);

    }

    private void DialogAccepted(object sender, SwipedEventArgs e)
    {

        if (isTaskingRunning || allDialogsAnswered) // isTaskingRunning = bool que valida se a animacao do dialogo tá sendo executada
        {
            return;
        }

		if (!CheckIfItsFirstTime())
		{
            tutorialComponent.IsVisible = false;
        }

        ChoiceMade(true);
    }

    private void DialogRefused(object sender, SwipedEventArgs e)
    {

        if (isTaskingRunning || allDialogsAnswered)
        {
            return;
        }

        if (!CheckIfItsFirstTime()) // esconde o componente de tutorial
        {
            tutorialComponent.IsVisible = false;
        }

        ChoiceMade(false);
    }

    private void PassDialog(object sender, EventArgs e) //método para pular a animação do diálogo 
    {
        userPassedAnimation = true; // var que atualiza se o usuario clicar na tela - significa que ele quer passar a animacao do dialogo
    }

    private async void NextEra(object sender, EventArgs e)
    {
        if (eraResponseGlobal.Id <= 6) { eraResponseGlobal.Id++; }

        EraResponse nextEraResponse = await eraService.GetEraByID(eraResponseGlobal.Id);

        if (nextEraResponse != null)
        {
            audioService.StopBackGroundAudio();
            await Navigation.PushAsync(new LoadingPage(usuarioDto, nextEraResponse, audioService));
        }
        else
        {
            await DisplayAlert("Erro", "Não foi possível carregar os dados da era.", "OK");
        }
    }

    private async void GameOverBtn(object sender, EventArgs e)
    {
        audioService.StopBackGroundAudio();
        await Navigation.PushAsync(new MainPage(this.audioService));
    }

    protected override bool OnBackButtonPressed() // cancela o botao de voltar do celular
    {
        return true;
    }
}
