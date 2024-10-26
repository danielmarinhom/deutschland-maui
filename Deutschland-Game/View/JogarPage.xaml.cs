using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using System.Diagnostics;
using Microsoft.Maui.Storage;

namespace Deutschland_Game.View;

public partial class JogarPage : ContentPage
{
	private UsuarioDto usuarioDto;

	private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponse;

	private EraResponse eraResponse;

    const string IsFirstLaunchKey = "IsFirstLaunch";

    public JogarPage(UsuarioDto usuarioDto, List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses, EraResponse eraResponse)
	{
		InitializeComponent();

		this.usuarioDto = usuarioDto;
		this.allDatasBeforeEraResponse = allDatasBeforeEraResponses;
		this.eraResponse = eraResponse;

		tutorialComponent.IsVisible = false;

		if (CheckIfItsFirstTime()) {

			tutorialComponent.IsVisible = true;

        }

		RunTextStyle();
		
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

	public async void RunTextStyle()
	{
		await LoadTextStyle("Senhor João", personagemNomeLabel, 100);
        await LoadTextStyle("Senhor, os impostos imperiais são sufocantes e estão  causando fome e miséria nas vilas. Precisamos de uma redução nos impostos.", dialogoLabel, 20);
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