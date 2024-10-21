using CommunityToolkit.Mvvm.ComponentModel;
using Deutschland_Game.Models;
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
        int id;

        public ICommand CadastrarUsuarioCommand { get; }

        public async Task CadastrarUsuario()
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Nome = nome,
                    Id = id
                };

                UsuarioService usuarioService = new UsuarioService();
                Usuario newUsuario = await usuarioService.CadastrarUsuarioAsync(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine($"erro ao cadastrar usuario - {e.Message}");
            }
            /*
             corrigir ao inves de Usuario newUsuario - > string idUsuario (pois a api retorna um id)
             */
        }
    }
}
