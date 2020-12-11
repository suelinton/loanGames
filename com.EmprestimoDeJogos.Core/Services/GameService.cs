using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using System.Collections.Generic;

namespace com.EmprestimoDeJogos.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GameEntity CreateGame(GameEntity game)
        {
            return _gameRepository.Add(game);
        }

        public IEnumerable<GameEntity> GetGames()
        {
            return _gameRepository.GetGames();
            
        }
    }
}
