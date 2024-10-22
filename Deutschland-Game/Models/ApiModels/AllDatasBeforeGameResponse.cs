using System.Text.Json.Serialization;
using Deutschland_Game.Dtos;

namespace Deutschland_Game.Models.ApiModels
{
    public class AllDatasBeforeEraResponse
    {

        [JsonPropertyName("id_dialogo")]
        public int IdDialogo { get; set; }

        [JsonPropertyName("mensagem")]
        public string mensagem { get; set; }

        [JsonPropertyName("personagem")]
        public PersonagemDto Personagem { get; set; }

        [JsonPropertyName("respostas")]
        public RespostasDto Respostas { get; set; }

        [JsonPropertyName("consequencias")]
        public ConsequenciasResponseDto Consequencias { get; set; }

    }
}
