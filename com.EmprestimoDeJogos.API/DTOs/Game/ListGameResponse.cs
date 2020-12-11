using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.EmprestimoDeJogos.API.DTOs.Game
{
    public class ListGameResponse
    {
        public List<GameDto> Games { get; set; } = new List<GameDto>();
    }
}
