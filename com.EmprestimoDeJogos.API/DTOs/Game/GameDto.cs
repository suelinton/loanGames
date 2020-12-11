using com.EmprestimoDeJogos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.EmprestimoDeJogos.API.DTOs.Game
{
    public class GameDto
    {
        public string Name { get; set; }
        public FriendEntity LoanedTo { get; set; }
    }
}
