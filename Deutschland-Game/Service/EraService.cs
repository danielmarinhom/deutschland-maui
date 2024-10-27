using Deutschland_Game.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deutschland_Game.Service
{
    public class EraService
    {

        private readonly HttpClient _httpClient;
        private JsonSerializerOptions serializerOptions;

        public EraService()
        {
            _httpClient = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<EraResponse> GetEraByID(int eraID)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                return null;
            }

            try
            {

                var response = await _httpClient.GetAsync($"{ApiBaseURL.API_BASE_URL}/era/id/{eraID}");


                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    var json = JsonSerializer.Deserialize<EraResponse>(content, serializerOptions);
                    return json;

                }

            }catch (Exception ex) { 
                Debug.WriteLine("ERRO -------" + ex.Message);
            }

            return null;
        }

        public async Task<string> DownloadImg64Async(string base64, string nome)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                return null;
            }
            byte[] imageBytes = Convert.FromBase64String(base64);
            string fileName = $"{nome}.png";
            string localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            await File.WriteAllBytesAsync(localPath, imageBytes);

            return localPath;
        }

    }
}
