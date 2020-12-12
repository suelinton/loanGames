using com.EmprestimoDeJogos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.EmprestimoDeJogos.Core.Interfaces
{
    public interface IFriendService
    {
        IEnumerable<FriendEntity> GetFriends();

        FriendEntity CreateFriend(FriendEntity friend);
    }
}
