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
		
	}
}