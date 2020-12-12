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
    }
}
