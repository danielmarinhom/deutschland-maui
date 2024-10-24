using Deutschland_Game.Dtos;
using Deutschland_Game.Models;
using Microsoft.Maui.Animations;
﻿using Deutschland_Game.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Deutschland_Game.Service
{
    public class UsuarioService
    {
        private HttpClient httpClient;
        private Usuario usuario;
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
        public async Task<UsuarioDto> CadastrarUsuarioAsync(String nome)
        {
            var usuarioDto = new UsuarioDto { Nome = nome };
            try
            {
                string json = JsonSerializer.Serialize(usuarioDto, jsonSerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uri + "/usuario/cadastrar"
                    ,content);
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
