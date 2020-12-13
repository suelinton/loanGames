using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.EmprestimoDeJogos.API.DTOs.Friend
{
    public class ListFriendResponse
    {
        public List<FriendDto> Friends { get; set; } = new List<FriendDto>();

    }
}
