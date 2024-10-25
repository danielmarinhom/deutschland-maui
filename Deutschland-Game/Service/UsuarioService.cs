using Deutschland_Game.Dtos;
using Deutschland_Game.Models;
using Microsoft.Maui.Animations;
﻿using Deutschland_Game.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

namespace Deutschland_Game.Service
{
    public class UsuarioService
    {
        private HttpClient httpClient;
        private Usuario usuario;
        private JsonSerializerOptions jsonSerializerOptions;
        private int userId;
        Uri uri = new Uri("http://192.168.15.200:8080");
        
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
        public async Task<UsuarioDto> CadastrarUsuarioAsync(String nome)
        {

            try
            {

                var response = await httpClient.PostAsJsonAsync("http://192.168.15.200:8080/usuario/cadastrar", new { Nome = nome});

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    UsuarioDto usuarioCadastrado = JsonSerializer.Deserialize<UsuarioDto>(jsonResponse, jsonSerializerOptions);

                    return usuarioCadastrado;
                }
                else
                {
                    Debug.WriteLine(response.StatusCode);
                    return null; 
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
