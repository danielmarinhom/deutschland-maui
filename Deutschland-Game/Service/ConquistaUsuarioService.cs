using Deutschland_Game.Dtos;
using Deutschland_Game.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deutschland_Game.Service
{
    public class ConquistaUsuarioService
    {

        private readonly HttpClient _httpClient;
        private JsonSerializerOptions serializerOptions;

        public ConquistaUsuarioService()
        {
            _httpClient = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<ConquistasResponseDto>> FindByUserID(long userID)
        {

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                return null;
            }

            var uri = new Uri($"{ApiBaseURL.API_BASE_URL}/conquistas/all/user/id/{userID}");

            try
            {
                var response = await _httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var json = JsonSerializer.Deserialize<List<ConquistasResponseDto>>(content, serializerOptions);
                    return json;
                }
                Debug.WriteLine("z");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERRO ----------------------------- " + ex.Message);
            }

            return null;

        }

        public async Task UpdateConquistas(List<ConquistasResponseDto> conquistaUsuarioResponses, long userID)
        {

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                return;
            }

            var uri = new Uri($"{ApiBaseURL.API_BASE_URL}/conquistas/update/userID/{userID}");

            try
            {

                await _httpClient.PutAsJsonAsync(uri, conquistaUsuarioResponses);

            }catch(Exception ex)
            {
                Debug.WriteLine("ERRO ----------------------------- " + ex.Message);
            }

        }

    }
}
