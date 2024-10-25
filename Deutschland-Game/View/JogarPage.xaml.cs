using Deutschland_Game.Dtos;

namespace Deutschland_Game.View;

public partial class JogarPage : ContentPage
{
	private UsuarioDto usuarioDto;

	public JogarPage(UsuarioDto usuarioDto)
	{
		InitializeComponent();
		this.usuarioDto = usuarioDto;
	}
}