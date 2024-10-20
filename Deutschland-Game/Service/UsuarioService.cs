using Deutschland_Game.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Deutschland_Game.Service
{
    internal class UsuarioService
    {
        private HttpClient httpClient;
        private Usuario usuario;
        //private ObservableCollection<x> x;
        private JsonSerializerOptions jsonSerializerOptions;
        private string userId = "";
        Uri uri = new Uri("http://localhost:8080");
        
        //  url - >  /usuario/cadastrar   @RequestBody UsuarioDTO - > (String nome) - > return ResponseEntity.ok(id);
        public UsuarioService()
        {
            httpClient = new HttpClient();
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<Usuario> CadastrarUsuarioAsync(Usuario usuario)
        {
            try
            {
                string json = JsonSerializer.Serialize<Usuario>(usuario, jsonSerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uri + "/usuario/cadastrar"
                    , content);
                if (response.IsSuccessStatusCode)
                {
                    userId = response.Content.ToString();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return usuario;     // ver com o guilherme sobre retornar a string do id
        }
    }
}
