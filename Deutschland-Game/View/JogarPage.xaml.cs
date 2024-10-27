using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using System.Diagnostics;
using Microsoft.Maui.Storage;
using System.Formats.Asn1;

namespace Deutschland_Game.View;

public partial class JogarPage : ContentPage
{
	private UsuarioDto usuarioDto;

	private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponse;

	private EraResponse eraResponse;

    const string IsFirstLaunchKey = "IsFirstLaunch";

	private int actIndexDialog = 0;

    public JogarPage(UsuarioDto usuarioDto, List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses, EraResponse eraResponse)
	{
		InitializeComponent();

		this.actIndexDialog = 0;
        this.usuarioDto = usuarioDto;
		this.allDatasBeforeEraResponse = allDatasBeforeEraResponses;
		this.eraResponse = eraResponse;

		tutorialComponent.IsVisible = false;

		if (CheckIfItsFirstTime()) { // se é a primeira vez

			tutorialComponent.IsVisible = true; // habilita o component de tutorial

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