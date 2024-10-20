using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deutschland_Game.Dtos
{
    class LoadingResponseDto
    {

        public int IdDialogo {  get; set; }

        public string mensagem {  get; set; }

        public PersonagemDto Personagem {  get; set; }

        public RespostasDto Respostas { get; set; }

        public ConsequenciasResponseDto Consequencias { get; set; }

    }
}
