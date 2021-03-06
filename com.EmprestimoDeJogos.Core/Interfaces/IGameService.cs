﻿using com.EmprestimoDeJogos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.EmprestimoDeJogos.Core.Interfaces
{
    public interface IGameService
    {
        IEnumerable<GameEntity> GetGames();
        GameEntity CreateGame(GameEntity game);
        GameEntity GetGame(int id);
        GameEntity Update(GameEntity game);
        int Delete(GameEntity gameEntity);
    }
}
