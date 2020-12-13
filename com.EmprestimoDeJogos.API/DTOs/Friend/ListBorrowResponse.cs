using com.EmprestimoDeJogos.API.DTOs.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.EmprestimoDeJogos.API.DTOs.Friend
{
    public class ListBorrowResponse
    {
        public List<GameDto> Games { get; set; } = new List<GameDto>();

    }
}
