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

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                return null; 
            }

            try
            {

                var response = await httpClient.PostAsJsonAsync($"{ApiBaseURL.API_BASE_URL}/usuario/cadastrar", new { Nome = nome});

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

        public async Task<bool> deleteUser(long userID)
        {

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                return false;
            }

            try
            {

                var response = await httpClient.DeleteAsync($"{ApiBaseURL.API_BASE_URL}/usuario/deletar/id/{userID}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

            }catch(Exception ex)
            {
                Debug.WriteLine("ERRO -----" + ex.Message);
            }

            return false;

        }
    }
}
