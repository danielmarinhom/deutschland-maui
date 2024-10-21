using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deutschland_Game.Dtos
{
    public class PersonagemDto
    {

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        
        [JsonPropertyName("sprite")]
        public string Sprite { get; set; } // em base64

    }
}
