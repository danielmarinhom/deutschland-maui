using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deutschland_Game.Models.ApiModels
{
    public class ConquistaUsuarioResponse
    {

        [JsonPropertyName("id_conquista")]
        public int IdConquista {  get; set; }

        [JsonPropertyName("valor_acresc")]
        public int ValorAcrescentado {  get; set; }

        [JsonPropertyName("id_usuario")]
        public int IdUsuario { get; set; }

    }
}
