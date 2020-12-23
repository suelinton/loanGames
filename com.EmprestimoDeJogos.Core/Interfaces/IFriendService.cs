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
        FriendEntity GetFriend(int id);
        bool Update(FriendEntity friend);
        bool Delete(FriendEntity friendEntity);
        void BorrowGame(int id, int idGame);
        IEnumerable<GameEntity> Borrows(int id);
    }
}
