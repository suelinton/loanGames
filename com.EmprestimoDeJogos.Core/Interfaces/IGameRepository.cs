using com.EmprestimoDeJogos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.EmprestimoDeJogos.Core.Interfaces
{
    public interface IGameRepository
    {
        IEnumerable<GameEntity> GetGames();
        GameEntity Add(GameEntity game);
        GameEntity GetGame(int id);
        GameEntity Update(GameEntity game);
        int Delete(GameEntity gameEntity);
    }
}
