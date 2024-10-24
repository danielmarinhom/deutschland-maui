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

        public async Task<double> CadastrarUsuario(string nome)
        {
            try
            {
                /*  se precisar criar o objeto do usuario caso adicione mais atributos:
                Usuario usuario = new Usuario
                {
                    Nome = nome,
                    Id = id
                };
                */

                UsuarioService usuarioService = new UsuarioService();
                UsuarioDto newUsuarioDto = await usuarioService.CadastrarUsuarioAsync(nome);
                double id = Convert.ToDouble(newUsuarioDto.Id);
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine($"erro ao cadastrar usuario - {e.Message}");
                return -1;
            } 
        }
        public ICommand SavePostCommand { get; }

        public CadastrarUsuarioViewModel()
        {
            //SavePostCommand = new Command(CadastrarUsuario);
        }
        /*
        public async void CadastrarUsuario()
        {
            Usuario usuario = new Usuario();
            Usuario newUsuario = new Usuario();
            usuario.Nome = nome;
            usuario.Id = id;
            UsuarioService usuarioService = new UsuarioService();
            newUsuario = await usuarioService.CadastrarUsuarioAsync(usuario);

            /*
             corrigir ao inves de Usuario newUsuario - > string idUsuario (pois a api retorna um id)
             
        }*/
        
    }
}
