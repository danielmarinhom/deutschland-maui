﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deutschland_Game.Dtos
{
    public class ConquistasResponseDto
    {

        [JsonPropertyName("id_conquistas")]
        public int IdConquista {  get; set; }

        [JsonPropertyName("nome_conquistas")]
        public string NomeConquista { get; set; }

        [JsonPropertyName("valor_acrescentado")]
        public double ValorAcrescentado { get; set; }

    }
}
