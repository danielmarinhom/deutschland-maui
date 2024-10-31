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
    private EraService eraService = new EraService();

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
<<<<<<< HEAD
    private int idEra;
=======

>>>>>>> 9341495cb2700e3a51842d5483a7b9c8e9755256
    private List<int> conquistasValues;

    public JogarPage(UsuarioDto usuarioDto, List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses, string eraImagePath, List<string> personagemImagesPaths, long eraId)

    {
        InitializeComponent();

        conquistasLabels = new List<Label> { populaidadeAdicional, igrejaAdicional, diplomaciaAdicional, economiaAdicional, exercitoAdicional };

        conquistasValues = new List<int> { 0, 0, 0, 0, 0};

        viewModel = new JogarPageViewModel();
        BindingContext = viewModel;
        viewModel.setEraPathInImage(eraImagePath);

        viewModel.GetAllConquistasByUserID(usuarioDto.Id);

        this.conquistaUsuarioService = new ConquistaUsuarioService();
        this.id = id;
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

            List<ConquistasResponseDto> conquistas = new List<ConquistasResponseDto>();
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
        if (actIndexDialog >= allDatasBeforeEraResponse.Count)
        {
            ShowEndGameDialog(); // Exibe a caixa de diálogo com os resultados
        }
        return conquistasValues;
    }

<<<<<<< HEAD
    public async void ShowEndGameDialog()
    {
        List<int> finalConquistasValues = endGame();

        string message = $"Popularidade: {finalConquistasValues[0]}\n" +
                         $"Igreja: {finalConquistasValues[1]}\n" +
                         $"Diplomacia: {finalConquistasValues[2]}\n" +
                         $"Economia: {finalConquistasValues[3]}\n" +
                         $"Exército: {finalConquistasValues[4]}";

        await DisplayAlert("Resultado Final", message, "OK");
        if(idEra <= 6) { idEra++; }
        EraResponse eraResponse = await eraService.GetEraByID(idEra);
        if (eraResponse != null)
        {
            await Navigation.PushAsync(new LoadingPage(usuarioDto, eraResponse, idEra));
        }
        else
        {
            await DisplayAlert("Erro", "Não foi possível carregar os dados da era.", "OK");
        }
    }

    public async void AdicionalAnimation(Label label)
=======
    public async void AdicionalAnimation(Label label) // animacao dos valores acrescentados nas conquistas
>>>>>>> 9341495cb2700e3a51842d5483a7b9c8e9755256
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

        isTaskingRunning = false; // usuario pode fazer interações dnv

    }

    public async void ChoiceMade(bool wasAceppt) // wasAceppt = se o dialogo foi aceito
    {

<<<<<<< HEAD
        if (actIndexDialog >= allDatasBeforeEraResponse.Count)
        {
            return;
        }

        AtualizarConquistas(wasAceppt);

=======
        Debug.WriteLine("1");
        AtualizarConquistas(wasAceppt);
        Debug.WriteLine("1.5");

>>>>>>> 9341495cb2700e3a51842d5483a7b9c8e9755256
        List<ConquistasResponseDto> conquistas = new List<ConquistasResponseDto>();

        if (wasAceppt) // filtra as consequencias certas
        {
            conquistas = allDatasBeforeEraResponse[actIndexDialog].Consequencias.aceito;
        }
        else
        {
            conquistas = allDatasBeforeEraResponse[actIndexDialog].Consequencias.recusado;
        }

        Debug.WriteLine("2");


        var ids = await viewModel.SetAdicionalValuesInConquistas(conquistas, conquistasLabels);

        Debug.WriteLine("3");


        for (int i = 0; i < ids.Count; i++) // pra rodar as animações de +50, -100 e etc
        {
            AdicionalAnimation(conquistasLabels[ids[i] - 1]);
        }

        Debug.WriteLine("4");


        await conquistaUsuarioService.UpdateConquistas(conquistas, usuarioDto.Id); // faz o update das conquistasUsuario pela api 

        Debug.WriteLine("5");


        Vibration.Vibrate(200); // vibração do celular pra decoração do jogo

        await RunAnswer(actIndexDialog, wasAceppt); // aqui roda o dialogo do rei como resposta

        Debug.WriteLine("6");


        actIndexDialog++; // aumenta o indice pra indicar o proximo dialogo a ser carregado

        if(actIndexDialog >= allDatasBeforeEraResponse.Count)
        {
            // se o indice do dialogo for >= ao tamanha da lista ---- é pra nao dar exception de index no allDatasBeforeEraResponse
            // basicamente significa que os dialogos já foram todos carregados
            allDialogsAnswered = true;
            return;
        }

        Debug.WriteLine("7");


        await Task.Delay(1500);

        await RunDialog(actIndexDialog);

        Debug.WriteLine("8");

    }

    private void DialogAccepted(object sender, SwipedEventArgs e)
    {

        if (isTaskingRunning || allDialogsAnswered) // isTaskingRunning = bool que valida se a animacao do dialogo tá sendo executada
        {
            return;
        }

		if (CheckIfItsFirstTime())
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

        if (CheckIfItsFirstTime())
        {
            tutorialComponent.IsVisible = false;
        }

        ChoiceMade(false);
    }

    private void PassDialog(object sender, EventArgs e) // método para pular a animação do diálogo 
    {
        userPassedAnimation = true; // var que atualiza se o usuario clicar na tela - significa que ele quer passar a animacao do dialogo
    }
}
