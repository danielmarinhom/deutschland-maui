using Deutschland_Game.Models.ApiModels;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace Deutschland_Game.Service
{
    internal class AllDatasBeforeEraService
    {

        private readonly HttpClient _httpClient;
        private JsonSerializerOptions serializerOptions;

        public AllDatasBeforeEraService()
        {
            _httpClient = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<AllDatasBeforeEraResponse>> GetDatasByEraID(int eraID)
        {
            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                Debug.WriteLine("SEM INTERNET!!!!!!");
                return null; 
            }

            var uri = new Uri($"{ApiBaseURL.API_BASE_URL}/allDatas/load/era/{eraID}");

            try
            {
                var response = await _httpClient.GetAsync(uri);
                Debug.WriteLine(response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var results = JsonSerializer.Deserialize<List<AllDatasBeforeEraResponse>>(content, serializerOptions);
                    return results;
                }

            }catch(Exception ex)
            {
                Debug.WriteLine("ERRO ----------------------------- " + ex.Message);
            }


            return null;
            

        }

    }
}
