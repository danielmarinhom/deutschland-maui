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
            //_httpClient.BaseAddress = new Uri(ApiDatas.API_BASE_URL);
        }

        public async Task<List<AllDatasBeforeEraResponse>> GetDatasByEraID(int EraID)
        {
            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                return null; // melhoria: lançar exception para tratar dps visualmente
            }

            var uri = new Uri($"http://192.168.15.200:8080/allDatas/load/era/1");

            try
            {
                var response = await _httpClient.GetAsync(uri);

                var data = await response.Content.ReadAsStringAsync();

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
