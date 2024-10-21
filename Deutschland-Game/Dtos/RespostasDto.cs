using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deutschland_Game.Dtos
{
    public class RespostasDto
    {

        [JsonPropertyName("aceito")]
        public string Aceito { get; set; }
        
        [JsonPropertyName("recusado")]
        public string Recusado {  get; set; }

    }
}
