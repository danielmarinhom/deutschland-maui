using CommunityToolkit.Mvvm.ComponentModel;
using Deutschland_Game.Dtos;
using Deutschland_Game.Models;
using Deutschland_Game.Service;
using System.Windows.Input;

namespace Deutschland_Game.ViewModel
{
    public partial class CadastrarUsuarioViewModel : ObservableObject
    {
        [ObservableProperty]
        string nome;
        [ObservableProperty]
        int id;

        public ICommand CadastrarUsuarioCommand { get; }

        public async Task<UsuarioDto> CadastrarUsuario(string nome)
        {
            try
            {

                UsuarioService usuarioService = new UsuarioService();
                return await usuarioService.CadastrarUsuarioAsync(nome);
            }
            catch (Exception e)
            {
                Console.WriteLine($"erro ao cadastrar usuario - {e.Message}");
                return null;
            } 
        }

    }
}
