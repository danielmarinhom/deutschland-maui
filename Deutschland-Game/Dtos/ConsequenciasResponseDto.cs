using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deutschland_Game.Dtos
{
    class ConsequenciasResponseDto
    {

        [JsonPropertyName("aceito")]
        public List<ConquistasResponseDto> aceito {  get; set; }

        [JsonPropertyName("recusado")]
        public List<ConquistasResponseDto> recusado {  get; set; }

    }
}
