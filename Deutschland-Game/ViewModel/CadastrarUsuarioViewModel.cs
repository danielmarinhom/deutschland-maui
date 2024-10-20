using CommunityToolkit.Mvvm.ComponentModel;
using Deutschland_Game.Model;
using Deutschland_Game.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Deutschland_Game.ViewModel
{
    public partial class CadastrarUsuarioViewModel : ObservableObject
    {
        [ObservableProperty]
        string nome;
        [ObservableProperty]
        double id;

        public ICommand SavePostCommand { get; }
        CreateCadastrarUsuarioViewModel()
        {
            SavePostCommand = new Command(CadastrarUsuario);
        }
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
             */
        }
    }
}
