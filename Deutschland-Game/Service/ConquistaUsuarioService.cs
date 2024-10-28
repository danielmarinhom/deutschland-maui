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

        public async Task<List<ConquistaUsuarioResponse>> FindByUserID(int userID)
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
                    var json = JsonSerializer.Deserialize<List<ConquistaUsuarioResponse>>(content, serializerOptions);
                    return json;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine("ERRO ----------------------------- " + ex.Message);
            }

            return null;

        }

        public async Task<bool> UpdateConquistas(List<ConquistaUsuarioResponse> conquistaUsuarioResponses)
        {

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                return false;
            }

            var uri = new Uri($"{ApiBaseURL.API_BASE_URL}/conquistas/update");

            try
            {

                var response = await _httpClient.PutAsJsonAsync(uri, conquistaUsuarioResponses);

                if(response.StatusCode == HttpStatusCode.NoContent)
                {
                    return true;
                }

            }catch(Exception ex)
            {
                Debug.WriteLine("ERRO ----------------------------- " + ex.Message);
            }

            return false;

        }

    }
}
