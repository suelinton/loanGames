﻿using com.EmprestimoDeJogos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.EmprestimoDeJogos.Core.Interfaces
{
    public interface IFriendRepository
    {
        IEnumerable<FriendEntity> GetFriends();
        FriendEntity Add(FriendEntity friend);
        FriendEntity GetFriend(int id);
        bool Update(FriendEntity friend);
        bool Delete(FriendEntity friendEntity);
        void Borrow(int id, int idGame);
        IEnumerable<GameEntity> GetBorrows(int id);
    }
}
