using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;

namespace Deutschland_Game.View;

public partial class JogarPage : ContentPage
{
	private UsuarioDto usuarioDto;

	private List<AllDatasBeforeEraResponse> allDatasBeforeEraResponse;

	private EraResponse eraResponse;

	public JogarPage(UsuarioDto usuarioDto, List<AllDatasBeforeEraResponse> allDatasBeforeEraResponses, EraResponse eraResponse)
	{
		InitializeComponent();
		this.usuarioDto = usuarioDto;
		this.allDatasBeforeEraResponse = allDatasBeforeEraResponses;
		this.eraResponse = eraResponse;
		RunTextStyle();
		
	}

	public async void RunTextStyle()
	{
		await LoadTextStyle("Senhor Jo�o", personagemNomeLabel, 100);
        await LoadTextStyle("Senhor, os impostos imperiais s�o sufocantes e est�o  causando fome e mis�ria nas vilas. Precisamos de uma redu��o nos impostos.", dialogoLabel, 20);
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
}