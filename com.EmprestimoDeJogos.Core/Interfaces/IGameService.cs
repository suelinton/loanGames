using com.EmprestimoDeJogos.Core.Entities;
using System.Collections.Generic;

namespace com.EmprestimoDeJogos.Core.Interfaces
{
    public interface IGameService
    {
        IEnumerable<GameEntity> GetGames();
        GameEntity CreateGame(GameEntity game);
    }
}
