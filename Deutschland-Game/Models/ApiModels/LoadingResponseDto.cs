using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Deutschland_Game.Dtos;

namespace Deutschland_Game.Models.ApiModels
{
    class LoadingResponseDto
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
