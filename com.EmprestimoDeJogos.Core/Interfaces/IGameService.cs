using com.EmprestimoDeJogos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.EmprestimoDeJogos.Core.Interfaces
{
    public interface IGameService
    {
        IEnumerable<GameEntity> GetGames();
        GameEntity CreateGame(GameEntity game);
        GameEntity GetGame(int id);
        void Update(GameEntity game);
        void Delete(GameEntity gameEntity);
    }
}
