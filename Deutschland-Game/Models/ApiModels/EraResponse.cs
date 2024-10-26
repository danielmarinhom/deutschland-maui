using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deutschland_Game.Models.ApiModels
{
    public class EraResponse
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome_era")]
        public string Nome {  get; set; }

        [JsonPropertyName("sprite")]
        public string Sprite { get; set; }

    }
}
